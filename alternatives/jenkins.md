# Jenkins

> This exercise assumes by default that you've already completed either the GitHub or GitLab instructions.

> Specifically, it assumes that you have already explored the application locally, and that you have a hosted repository version of the code that you can push to

### Step 1 - Run Jenkins locally
There are two options for running Jenkins locally, you can either install Jenkins or run it through Docker. We would recommend running Jenkins through Docker and the instructions for that are [here](https://www.jenkins.io/doc/book/installing/#docker).

> If you're on Windows make sure to run the commands in PowerShell rather than Git Bash to avoid issues with path expansion.

### Step 2 - Set up Jenkins
Once you've done the step above you should have Jenkins running on <http://localhost:8080/>. If you go to this url in a browser it should show you a setup page.
1. Login with the password you got from the logs when starting Jenkins. **Hint:** You can run `docker logs your_container` to access a container's logs. Run `docker container ls` to view a list of running containers.
2. Now you have the option to select some initial plugins. Either select the suggested plugins or if you customise it, make sure you include the "Docker" plugin. We won't need any others right away, and you can add more later.
3. Create an admin user.
4. Use the default Jenkins URL (<http://localhost:8080>)

You should now see the Jenkins dashboard.

### Step 3 - Set up a Jenkins build
We now want to get Jenkins to build our app. To do this you need to create a job on Jenkins for our app and create a [Jenkinsfile](https://www.jenkins.io/doc/book/pipeline/jenkinsfile/) in your repository to define what the job should do.

#### Create a Jenkins job
From your Jenkins dashboard:
1. Select New Item.
2. Set it to a multibranch pipeline. This means the job will scan your repository for branches and run the job on any branch with a Jenkinsfile.
3. Leave all the defaults other than setting the branch sources to GitHub. Leave the defaults for the branch source other than setting the repository url to your repository url. You may notice a warning about not using GitHub credentials at this point. This is fine, as we're just reading from a public repository we don't need credentials. If we were using a private repository or we were writing to the repository during the job, then we would need to set up credentials.
4. Click Save to create the Jenkins job.

If the initial scan that it runs seems to be slow, see the hint below for how to adjust GitHub rate limiting.

#### Create a Jenkinsfile
See <https://www.jenkins.io/doc/book/pipeline/jenkinsfile/> for details on how to create a Jenkinsfile. We want to add the same steps as for the GitHub Actions workflow so that it:
1. Builds the C# code.
2. Runs the C# tests.
3. Builds the TypeScript code.
4. Runs the linter on the TypeScript code.
5. Runs the TypeScript tests.

You have 2 options for installing .NET Core & npm inside Jenkins:
1. Make installation separate build stages
    * This is not ideal as you will have to run the installation on each build
2. [Specify containers to run stages of the Jenkins pipeline with .NET Core and npm pre-installed](https://www.jenkins.io/doc/book/pipeline/docker/)
    * The simplest approach is to have one stage for your `npm` commands and a second stage for your `dotnet` commands, because each stage can have its own agent. You will have to specify `agent none` at the top of the pipeline.
    * There are some pre-built images for npm (e.g. `node:17-bullseye`)
    * Similarly for .NET you can use [Microsoft's image](https://hub.docker.com/_/microsoft-dotnet-sdk). You may need to set some environment variables in your Jenkinsfile for the dotnet CLI to work correctly: `DOTNET_CLI_HOME` (e.g. to `"/tmp/dotnet_cli_home"`) and `XDG_DATA_HOME` (e.g. to `"/tmp"`).

<details>
<summary>Hints</summary>

* You'll need to use a `dir` block for some steps to run them inside the `DotnetTemplate.Web` directory.
* If Jenkins starts rate limiting your repository scanning you can go to "Manage Jenkins" -> "Configure System" and change "Github API usage rate limiting strategy" to "Throttle at/near rate limit". Adding credentials to your pipeline configuration will also increase the limit.

</details>

#### Run the Jenkins job
1. Commit and push your new Jenkinsfile.
2. From your Jenkins dashboard select the job you created.
3. Click "Scan Repository Now" (exact wording may vary). This will scan the repository for branches and run the job on any branch with a Jenkinsfile.
4. Select your branch, which should appear under "Branches" once the scan is done.
5. You should see a stage view of the build, showing each stage in the Jenkinsfile. If the stage succeeded it will be green, if it failed it will be red.
6. Select the most recent build from the build history on the left.
7. Click "Console Output" to view the full logs from the build.

Once the repository scanning has picked up a branch you can build it again by clicking "Build Now" for that branch. You only need to scan the repository to check for new branches with Jenkinsfiles.

### (Stretch goal) Code coverage
We want high _test coverage_, meaningfully testing as much of the functionality of the application as possible. _Code coverage_ is a more naive metric - it simply checks which lines of code were executed during the test run. But higher code coverage is usually a good thing and it can still usefully flag which parts of the codebase are definitely untested. So let's include code coverage in our CI pipeline.

First check it works manually. From the DotnetTemplate.Web folder, run the command `npm run test-with-coverage`. This runs the frontend tests and calculates code coverage at the same time.

It produces two reports: one in HTML form that you can open in your browser (DotnetTemplate.Web/coverage/index.html) and one in XML that we will get Jenkins to parse.

Try adding code coverage to Jenkins:

1. Install the [Code Coverage API](https://plugins.jenkins.io/code-coverage-api/) plugin on Jenkins.
2. Change your Jenkins pipeline to run the tests with code coverage.
3. Add a step after running the tests to publish coverage. You can see a simple example of the command if you scroll down the Code Coverage API documentation, to the "pipeline example". You will want to use the "istanbulCoberturaAdapter", and the report to publish is "cobertura-coverage.xml" in the coverage folder.
4. You should see a code coverage report appear on the build's page after it completes. Click through to see details.

Now let's enforce high code coverage:

1. Configure it to fail the build for code coverage below 90%. You may find it easiest to use the Jenkins [Snippet Generator](https://www.jenkins.io/doc/book/pipeline/getting-started/#snippet-generator).
2. Push your change and watch the build go red!
3. Edit the `DotnetTemplate.Web/Scripts/spec/exampleSpec.ts` file:
  * Update the import statement: `import { functionOne, functionTwo } from '../home/example';`
  * Invoke functionTwo on a new line in the test `functionTwo();`
4. Push the change and observe the build go green again! You can also view the code coverage history.

### (Stretch goal) Slack notifications
Like for the GitHub Actions workflow, add slack notification to the Jenkins job. To make this work you will need to use the Slack app [Jenkins CI](https://slack.com/apps/A0F7VRFKN-jenkins-ci?next_id=0), make sure this has been installed in the slack workspace you're using.

> Note that their documentation may be slightly out of date and not quite match the page you see in Jenkins.

### (Stretch goal) Use a Single Build Agent for the Jenkins Pipeline
Can you create a single container that can be used as the sole build agent for the entire multistage Jenkins pipeline? You might need to do this to run end to end tests for example.
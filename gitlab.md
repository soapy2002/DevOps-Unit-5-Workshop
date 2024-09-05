## Part 1 (GitLab CI)

### Step 1 - Get the code

- Sign in to GitLab and create a new project/repository
- Select "Import project"
- Select "Repo by URL"
- Enter the URL of this repository and omit any username/password (credentials aren't required because this repository is public).

Finish creating the project and then you should be able to clone your new repository.

### Step 2 - Set up the app

Follow the instructions in the [Running Locally](./run_locally.md) section to try running the application locally. **If you encounter issues running the app locally, feel free to skip to step 3 instead of spending time resolving the issues.** The only benefit of running it locally first is to better understand what you want your pipeline to replicate.

### Step 3 - Set up GitLab CI

1. Create the config file for your continuous integration pipeline:
    * Create a file called `.gitlab-ci.yml` in the root of the project
2. Implement a basic workflow:

    ```yml
    stages:
      - test    # This pipeline consists of a single stage called "test"

    my-job:         # Start defining a job called "my-job"
      stage: test   # This job belongs to the "test" stage
      script:
        - echo "Hello, world"  # Run a script
    ```

3. Commit and push your changes to a branch on your repository.
4. On your repository page, navigate to the CI/CD tab.
5. You should see a table of pipelines. This should have one entry with a name matching your latest commit message. Select this entry.
6. You should see a page with a visual representation of your pipeline, you can click on a job to see the detailed console output.

See [the GitLab documentation](https://docs.gitlab.com/ee/ci/quick_start/) for more details on how to get set up GitLab CI/CD and <https://docs.gitlab.com/ee/ci/yaml/index.html> for more details on the syntax of the `gitlab-ci.yml` file.

### Step 4 - Implement the CI pipeline

#### Validate the C# code

First, we want the CI pipeline to build the C# code and run the C# tests. This requires having the .NET SDK installed. Instead of scripting the installation of .NET on the runner, we can run the job inside a container that already has it installed. To do this, add `image: desired-image-name-here` to your job, but with an appropriate image name. Find the correct tag for [Microsoft's image](https://hub.docker.com/_/microsoft-dotnet-sdk).

Amend the job in your workflow file so that it:

1. Uses a Docker image that has the .NET SDK installed
2. Builds the C# code.
3. Runs the C# tests.

Update the script to run the two commands `dotnet build` and `dotnet test`. Push your changes and check that the pipeline succeeds.

#### Validate the TypeScript code

Next, we want the pipeline to validate the TypeScript code. You need a different image (with Node installed instead of .NET) so create a second job with an appropriate image. This new job can belong to the same stage as the first one, because they do not depend on each other and can happily run in parallel.

Fill in the "script" section with the correct commands so that it:

1. Uses a Docker image with Node installed
2. Installs dependencies and build the TypeScript code.
3. Runs the linter on the TypeScript code.
4. Runs the TypeScript tests.

Remember that you want to run the `npm` commands from the DotnetTemplate.Web directory.

### (Stretch goal) Slack notifications

To make sure people are aware when there are issues with the build, it can be useful to send a Slack notification at the end of the workflow.

**Before attempting this step please create your own personal slack workspace. This is free and can be set up [here](https://slack.com/create).**

Take a look at the [GitLab Slack integration](https://docs.gitlab.com/ee/user/project/integrations/slack.html).

### (Stretch goal) Pipeline status badge

Add a [pipeline status badge](https://docs.gitlab.com/ee/user/project/badges.html) to your repository.

### (Stretch goal) Change when the workflow is run

Change your workflow so that it only runs when pushing to the main branch or by raising a Merge Request. Is there a way to ensure that no one can update the main branch except through a PR that has passed the workflow?

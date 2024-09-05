## Part 1 (GitHub Actions)

### Step 1 - Get the code

We'll be working on top of this repository. Because you'll need to change some settings on the repository later in the workshop, it's recommended that you [fork](https://docs.github.com/en/github/getting-started-with-github/fork-a-repo) the repository instead of cloning it. To do that:
1. Click the Fork button in the top right on the repository page.
2. Select your GitHub user when it asks you where you should fork it to.
3. This should take you to a fork of the repository on your account, e.g. <https://github.com/MyUser/DevOps-Unit-5-Workshop> where MyUser will be replaced by your username.
4. You can now clone and push to that repository as normal.

### Step 2 - Set up the app

Follow the instructions in the [Running Locally](./run_locally.md) section to try running the application locally. **If you encounter issues running the app locally, feel free to skip to step 3 instead of spending time resolving the issues.** The only benefit of running it locally first is to better understand what you want your pipeline to replicate.

### Step 3 - Set up GitHub Actions

1. Create the config file for your continuous integration pipeline:
    * Create a folder called ".github" at the root of the repository.
    * Inside there, create a "workflows" folder.
    * Inside there create a file: you can name it whatever you like, although it needs to have a .yml extension, e.g. continuous-integration-workflow.yml.
2. Implement a basic workflow:
```yaml
name: Continuous Integration
on: [push]                      # Will make the workflow run every time you push to any branch

jobs:
  build:
    name: Build and test
    runs-on: ubuntu-latest      # Sets the build environment a machine with the latest Ubuntu installed
    steps:
    - uses: actions/checkout@v4 # Adds a step to checkout the repository code
```
3. Commit and push your changes to a branch on your repository.
4. On your repository page, navigate to the Actions tab.
5. You should see a table of workflows. This should have one entry with a name matching your latest commit message. Select this entry.
6. On the next page click "Build and test" on the left. This should show you the full output of the workflow which ran when you pushed to your branch. See [the documentation](https://docs.github.com/en/actions/configuring-and-managing-workflows/managing-a-workflow-run) for more details on how to view the output from the workflow.

See [the GitHub documentation](https://docs.github.com/en/actions/configuring-and-managing-workflows/configuring-and-managing-workflow-files-and-runs) for more details on how to set up GitHub Actions and <https://docs.github.com/en/actions/reference/workflow-syntax-for-github-actions> for more details on the syntax of the workflow file.

### Step 4 - Add more actions
Currently our workflow only checks out the code, which isn't that useful. We want to add some more useful steps to the workflow file. Each step in the workflow file either needs to:
* Specify `run` to run a command as you would in the terminal, for example:
```yaml
name: Continuous Integration
on: [push]

jobs:
  build:
    name: Build and test
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v4


    - name: Hello world       # Name of step
      run: echo 'Hello world' # Command to run
```
* Specify `uses` followed by the name of the action. The name of the action is of the form `GitHubUsername/RepositoryName` and you can find them by searching the [marketplace](https://github.com/marketplace?type=actions). Anyone can publish actions - you could create your own or fork an existing one. If it is supplied by GitHub themselves, the username will be `actions`. For example:
```yaml
name: Continuous Integration
on: [push]

jobs:
  build:
    name: Build and test
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v4

    - name: Hello world
      uses: actions/hello-world-javascript-action@v1.1 # Name of the action. This uses https://github.com/actions/hello-world-javascript-action
      with:                                            # This section is needed if you need to pass arguments to the action
        who-to-greet: 'Mona the Octocat'
```

You should amend your workflow file so that it:
1. Builds the C# code.
2. Runs the C# tests.
3. Builds the TypeScript code.
4. Runs the linter on the TypeScript code.
5. Runs the TypeScript tests.

### (Stretch goal) Slack notifications

To make sure people are aware when there are issues with the build, it can be useful to send a Slack notification at the end of the workflow.

**Before attempting this step please create your own personal slack workspace. This is free and can be set up [here](https://slack.com/create).**

1. Add a slack notification at the end of the workflow. To make this work you will need to use the slack app [incoming webhooks](https://softwire.slack.com/apps/A0F7XDUAZ-incoming-webhooks?next_id=0), make sure this has been installed in the slack workspace you're using.
2. Make the workflow post a different message if the workflow failed, so that it's obvious if the workflow failed.
3. Make the workflow post a different message if the workflow was cancelled.

You should not commit your webhook URL to the repository, instead store it in a [repository secret](https://docs.github.com/en/actions/security-guides/encrypted-secrets#creating-encrypted-secrets-for-a-repository), and [read it in your pipeline](https://docs.github.com/en/actions/security-guides/encrypted-secrets#using-encrypted-secrets-in-a-workflow).

### (Stretch goal) Workflow status badge

Add a [workflow status badge](https://docs.github.com/en/free-pro-team@latest/actions/managing-workflow-runs/adding-a-workflow-status-badge) to your repository.

### (Stretch goal) Change when the workflow is run

Change your workflow so that it only runs when pushing to the main branch or by raising a PR. Is there a way to ensure that no one can update the main branch except through a PR that has passed the workflow?

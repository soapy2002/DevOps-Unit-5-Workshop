# DevOps Unit 5 Workshop

Welcome to the Unit 5 Workshop!

## Our objectives
The objectives for today are:

- Get more comfortable setting up a CI pipeline, even for an unfamiliar project
- Understand a variety of tasks that can be put into a CI pipeline
- See how GitHub Actions or GitLab can be used to build a CI pipeline that accomplishes the above
- See how Azure DevOps can be used to build a CI pipeline that accomplishes the above

## Pre-flight checks
Today we'll be building a CI pipeline for a minimal C# application. Largely speaking, this means we're going to be writing instructions in a special format that tells a CI tool how to build, test, and (generally speaking) deploy our application. You don't need to worry about how the code works, the objective of today is to work with CI, and we only really need a text editor (or VS Code) for that. However, it is useful to be able to locally execute what we would want the CI pipeline to do, because if we know what the commands and their output looks like when run locally, that helps us understand if our instructions for the CI pipeline are also working correctly. Accordingly, it will be useful to be able to have .NET installed, as well as npm. This can either be on your machine, or on an ACG instance.

The backend is a minimal C# application, and requires the .NET Core tooling to build, run, and test. Therefore, if we want to do any of that locally (and ideally we would), we need to ensure that .NET is installed properly.
- .NET is installed properly if you can run `dotnet --version` and see a version number. 

The frontend is a minimal JavaScript application, and requires npm to build, test, and lint (run static analysis). Similarly, if we want to do any of that locally, we need to ensure that npm is installed properly.
- npm is installed properly if you can run `npm --version` and see a version number.

### If you are missing .NET
If you are missing .NET, you can install it by following the instructions here: https://dotnet.microsoft.com/download - or if you are using an ACG instance, you can run the following command inside it:
```bash
sudo apt update && sudo apt install -y dotnet-sdk-8.0
```

### If you are missing npm
If you are missing npm, you can install it by following the instructions here: https://docs.npmjs.com/downloading-and-installing-node-js-and-npm - or if you are using an ACG instance, you can run the following command inside it:
```bash
sudo apt update && sudo apt install -y npm
```

### Text editing in ACG
If you are using an ACG instance, you can continue to use the `nano` text editor to edit files. However, if you would prefer to use VS Code on ACG again like we did last workshop, you can follow the same instructions to install and run VS Code on your ACG instance.

## Morning
Today we're going to be building a CI pipeline for an application. The application is in this repository, and it is a simple C# application that has a frontend and a backend. The frontend will use npm to build, and the backend will use .NET to build. So, those are the tools that will be invoked in the CI pipeline.

For the morning, you have two choices! You can either build this CI pipeline using GitHub Actions, or you can build it using GitLab CI. The instructions for both are provided here:

- [GitHub Actions instructions](github_actions.md)
- [GitLab CI instructions](gitlab.md)

## Afternoon
- [Azure DevOps instructions](azure_devops.md)


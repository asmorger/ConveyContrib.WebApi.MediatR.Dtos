# ConveyContrib.WebApi.MediatR.Dtos

Opinionated MediatR + Dto layer integration for [Convey](https://convey-stack.github.io/).

## Overview
Convey is a coordinated set of helper methods developed by [DevMentors](https://devmentors.io/).  They provide a really innovative way of creating standards-based integrations for common tools and methodologies.

## Why this project?
Vladimir Khorikov has some valuable perspectives on CQRS + DDD [here in his blog](https://enterprisecraftsmanship.com/posts/cqrs-commands-part-domain-model/) and in his [Pluralsight course](https://app.pluralsight.com/library/courses/cqrs-in-practice/).  One topic that he brought up is using Commands as DTOs in your project.  He generally discourages the practice, although he does outline some limited scenarios where it is acceptable.  This is an attempt to bring that type of project structure to Convey.

## Opinions
This particular solution is specific to my needs and my opinions.  YMMV.

Dependencies:

* Automapper
* Fluent Validation
* Mediatr

## Process
Request -> Validate DTO (optional return) -> Map DTO to MeditatR Request -> Standard MediatR process

# Getting Started

## Installation
`dotnet add package ConveyContrib.WebApi.MediatR.Dtos`

## Usage

Extend your `Program.cs` startup to include Convey's `AddWebApi()` to add their required services.  Also use `.AddDtos()` to add the `MediatR + DTO` infrastructure.

```
WebHost.CreateDefaultBuilder(args)
  .ConfigureServices(services =>
      services
          .AddConvey()
          .AddWebApi()
          .AddDtos()
          .Build()
```

To define custom endopints, the process is the same as `Convey's`, except for the method name used.  Subsitute `UseEndpoints()` with `UseDtoEndpoints()`.  

```
app
    .UseDtoEndpoints(endpoints => endpoints
        .Post<TestDto, TestMessageWithResponse, int>("/test")
    );
```

# FAQ

## How does it work?
I would encourage you to go through the [Convey WebAPI Getting Started Guide](https://convey-stack.github.io/documentation/Web-API/) to see how their current implementation works.  I did my best to stick as closely to their implementation as I could for the sake of consistency.

## Design considerations
Because I'm integrating with someone else's library, I have tried to ensure there won't be any usage/naming conflicts with their implementations.  As such, I've used their builder system, but extended my own methods off of it.

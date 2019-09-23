# Course Home


## Discussions and questions

There is a chat in Gitter, click [![Join the chat at https://gitter.im/kmaooad18/course-home](https://badges.gitter.im/kmaooad18/course-home.svg)](https://gitter.im/kmaooad18/course-home?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge) to join. There are several channels there. This repo channel (course-home) is for discussing lectures, theoretical issues, general ideas on topic (OOAD), and for _important_ organizational announcements. **Please, ask common questions in this public channel, not in personal chat, so that everyone could benefit from the discussion. For assignment-specific discussions, use their own channels (gitter button is in every assignment repo). Don't ask assignment questions in general channel.**

## Issues and contribution

If you find any issue or potential improvement (with texts, in starter code etc) and you think you can fix it – that is super-welcome! Just go ahead and with a pull request. Otherwise feel free to post an issue in the corresponding repository.

## Assignments

Every week assignment deadline is 01:00 AM on next Wednesday.

Assignments are assessed by successful run of _all_ tests from Spec.fs (you can check that locally in advance — see [below](#implementing-assignment)). Check your results on [builds summary](/builds.md) page. Green build badge means your solution passes all tests on build server. This always reflects the latest available build and you can use this page as a mark of state of your repository. To get max points for your solution you have to provide passing build *by deadline*. Only the latest commit _before_ the deadline is checked for passing build. Same applies to extended deadline to get half points. Bottom line is: it does not matter if you had passing build some time before deadline, but broke it right before the deadline! Only last commit always matters. That is how it works in real life, so try to be careful from the start. 

[Week 4](https://classroom.github.com/a/eQ5nklTc) (Deadline 02/10)

### Getting assignments

1. Create your personal GitHub account (if you don't have one)
2. Login to GitHub and follow one of the assigment links above
3. On your first opening some assignment, GitHub will request access from Classroom to your account — that's ok, just grant access.
4. As you accept the assignment, GitHub will create your **personal private repository** and put starter code for assignment there. Your repository will be named like 'coding2019W4-*your_nickname*' etc. **Make sure your repository name matches the mentioned pattern, it is absolutely required to match you to your repo and grade properly!**
5. Use your personal repository from the previous step to push code of your solution for assignment.

### Implementing coding assignments

Most assignments are about coding.

1. Assigments are implemented in F# language and .NET Core 2.1 or later. 
2. To be able to develop, build, and run assignments you will need to install [.NET Core 2.1 SDK](https://www.microsoft.com/net/download/dotnet-core/2.1). MacOS also requires Mono to use F# - see [https://fsharp.org/use/mac/](https://fsharp.org/use/mac/).
3. You are free to use any editor you like or even go pure command-line, but the recommended IDE is [Visual Studio Code](https://code.visualstudio.com/download) (it's also cross-platform, it's nice, lightweight, simple, yet powerful). There is also a [portable version](https://code.visualstudio.com/docs/editor/portable) that does not need installation. For convenient use of F# in Visual Studio Code it's highly recommended to install [Ionide extension](http://ionide.io/).
4. To build the assignment, clone your repository from GitHub, in VS Code terminal or plain command line go to project folder (you need one with .fsproj file in it), and execute `dotnet build`, or you may use `dotnet run` to build and run at once.
5. Assignment comes as a starter code, typically as client code in Client.fs and Spec.fs file with specification tests.
6. You need to fill provided file(s) with your implementation so that the program does what's expected. Starter client code in contains type stubs, method stubs etc. You need to implement provided methods and/or add more of your own, if necessary.
7. A good sign of properly implemented program (in terms of spec) is successful run of tests from Spec.fs. To check those, execute `dotnet test` or `dotnet xunit`. You'll see if some requirements do not hold — test(s) will fail.
8. Don't try to cheat the tests: there might be additional tests on the build server to make sure your implementation is the _real_ one, not just the tests-cheater :)
9. Commit and push your progress to GitHub regularly: local PC crash sometimes. Make sure you commit and push your work in advance before the deadline. **Last commit _before_ the deadline will be taken for assessment.**

## Using F# 

The quickest way to get into F# (having knowledge of programming in other languages) is to follow cool tutorial at [https://try.fsharp.org](https://try.fsharp.org). 

There is also a nice [F# Cheatsheet](https://dungpa.github.io/fsharp-cheatsheet/) with almost all available language features and syntax highlights. 

Finally, offical [F# documentation](https://docs.microsoft.com/en-us/dotnet/fsharp/) is well-written and more than enough for the needs of our course. 

Remember, we are not learning language itself here, but only use it as a convenient tool for learning software design. So please feel free to ask questions in Gitter if you feel any discomfort about F#. 

## Reading & more info

There is no such thing as "The Design Book", no book or even several books exist that could be a substitute for this course or exhaustive body of knowledge on the topic. Yet there are good books - some really considered *classics* today – and they can help to build *initial* body of knowledge. Some of the mentioned books are refered during the course.

**Good general books about principles, patterns, and methods**

[Lar] Craig Larman, "Applying UML and Patterns: An Introduction to Object-Oriented Analysis and Design and Iterative Development (3rd Edition)"

[Mar] Robert C. Martin, "Agile Principles, Patterns, and Practices in C#"

[GoF] Erich Gamma, John Vlissides, Ralph Johnson, and Richard Helm, "Design Patterns: Elements of Reusable Object-Oriented Software"

[Fow] Martin Fowler, "Patterns of Enterprise Applications Architecture" ([book website](https://www.martinfowler.com/books/eaa.html), [patterns catalog](https://martinfowler.com/eaaCatalog/))

[Ddd] Eric Evans, "Domain-Driven Design: Tackling Complexity in the Heart of Software"

[See] Mark Seemann, "Dependency Injection in .NET, Second Edition"

[Ver] Vaugn Vernon, "Implementing Domain-Driven Design"

**Some advanced/ad-hoc sources, including architecture and technology**

[https://blog.ploeh.dk](https://blog.ploeh.dk) Mark Seemann blog. Advanced reading, not easy, addresses tricky issues and advanced topics. Highly recommended.

"CQRS Journey: Exploring CQRS and Event Sourcing" (by Microsoft team) [website](https://docs.microsoft.com/en-us/previous-versions/msp-n-p/jj554200(v=pandp.10)) 

Gregor Hohpe, Bobby Woolf, "Enterprise Integration Patterns : Designing, Building, and Deploying Messaging Solutions" ([book website](https://www.enterpriseintegrationpatterns.com/) with additional info)

[eShopOnWeb](https://github.com/dotnet-architecture/eShopOnWeb) - cool sample application from Microsoft showcasing typical usage of EF Core, ASP.NET Core, patterns etc]

[eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers) - similar to above, but much more advanced version: with microservices, containers, message bus etc.



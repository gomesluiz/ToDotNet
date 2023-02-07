# ToDotNet

A to-do application written in the C# programming language. It was built for software security teaching purposes, 
therefore it is deliberately vulnerable to:

* Sql-injection
* Cross-site scripting 

The vulnerabilities fixing was implemented on the following branches:

* fix/sql-injection
* fix/xss 

## Installation

```bash
C:\> git clone git@github.com:gomesluiz/ToDotNet.git
```

```bash
C:\> cd ToDotNet
C:\ToDotNet> dotnet tool install --global dotnet-ef
C:\ToDotNet> dotnet ef migrations add InitialMigration
C:\ToDotNet>dotnet add package HtmlSanitizer --version 8.0.645
```

```bash
C:\ToDotNet> dotnet ef database update
```

## Usage

```bash
C:\ToDotNet> dotnet run
```

To access the application, click on [http://localhost:5096](http://localhost:5096).


## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
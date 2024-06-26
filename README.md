# CV Parser Project - Backend

## Overview
This project is a RESTful API service that parses CV documents in PDF format, extracts key information, and stores it in a MongoDB database. It's designed to streamline the process of managing applicant information for recruitment purposes.

## Technologies Used

- `C# (.NET Core)`: Primary language and framework for backend development.
- `ASP.NET Core Web API`: Framework for building RESTful HTTP services.
- `MongoDB`: NoSQL database for flexible and scalable data storage.
- `iTextSharp`: Library for parsing and manipulating PDF documents.
- `JSON (appsettings.json)`: Configuration management for application settings.
- `RESTful API Architecture`: Design principle for scalable and stateless services.
- `MVC Pattern`: Architectural pattern for separating concerns in the application.

## Features
### PDF Upload
- Accepts PDF files containing applicant CVs.

### CV Parsing
- Extracts key information from PDFs, including:
  - Applicant ID
  - Name
  - Family Name
  - Email
  - LinkedIn URL
  - Mobile Phone Number
  - Personal Identification Number

### Data Storage
- Stores extracted information in MongoDB.

### Data Deletion
- Allows removal of applicant records from the database.


## Database and Data Modeling Approach

### Rationale for Choosing Schema-Less Approach

Given the requirements of the project, a schema-less approach using MongoDB is more appropriate for the following reasons:

#### Flexibility
- **Evolving CV Formats**: Allows the system to adapt to varying CV formats without requiring significant changes to the database schema.
- **Dynamic Fields**: New fields can be easily added as needed without schema alterations.

#### Development Speed
- **Rapid Prototyping**: Faster development and iteration since changes to the data model do not require schema migrations.

#### Scalability
- **Handling Large Volumes**: MongoDB scales horizontally, making it suitable for handling a growing number of CVs and associated data.

#### Data Variety
- **Unstructured Data**: Capable of storing unstructured or semi-structured data, common in CVs with various text formats and sections.

In conclusion, while a schema-based approach offers strong data integrity and support for complex relationships, the flexibility and scalability of a schema-less approach make it a better fit for this project, where data formats are not uniform and can evolve over time.


## API Endpoints

- **POST /api/applicants**: Upload and parse a new CV
- **GET /api/applicants**: Retrieve all applicant records
- **GET /api/applicants/{id}**: Retrieve a specific applicant's information
- **DELETE /api/applicants/{id}**: Remove an applicant's record

## Installation
### MongoDB

1. **Download MongoDB**:
   - Go to the [MongoDB Download Center](https://www.mongodb.com/try/download/community).
   - Choose your operating system and download the appropriate installer.

2. **Install MongoDB**:
   - Run the downloaded installer and follow the installation instructions.
   - For Windows, ensure the "Install MongoDB as a Service" option is checked.
   - For macOS, you can also use Homebrew:
     ```sh
     brew tap mongodb/brew
     brew install mongodb-community@5.0
     ```

3. **Start MongoDB**:
   - If MongoDB is installed as a service, it will start automatically.
   - For manual start:
     ```sh
     mongod
     ```

4. **Verify Installation**:
   - Open a terminal or command prompt.
   - Run the Mongo shell:
     ```sh
     mongo
     ```
   - You should see the MongoDB shell prompt.

### Project Setup

 **Open the Project:**

1. Open Visual Studio.
2. Go to `File` > `Open` > `Project/Solution` and select your project file (.sln).

 **Configure `appsettings.json`:**

Ensure your `appsettings.json` file includes the necessary configuration settings for MongoDB:

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://127.0.0.1:27017",
    "DatabaseName": "CVParserDB"
  },
}
```

### iTextSharp

1. **Add iTextSharp to Your Project**:
   - Open your .NET Core project in your preferred IDE (e.g., Visual Studio).

2. **Install iTextSharp via NuGet Package Manager**:
   - In Visual Studio:
     - Right-click on your project in Solution Explorer.
     - Select "Manage NuGet Packages".
     - Search for `itext7` or `itext7.pdfhtml` (for more features).
     - Click "Install".

   - Or, install via the Package Manager Console:
     ```sh
     Install-Package itext7
     ```
     or
     ```sh
     Install-Package itext7.pdfhtml
     ```

3. **Verify Installation**:
   - Ensure the `iText7` package is listed under the "Dependencies" section of your project in Solution Explorer.



## Author

**Shulamit Nahon**

I am Shulamit Nahon, a recent graduate in Computer Science from `JCT` (Jerusalem College of Technology). I am passionate about technology and software development, and I am currently seeking my first job in the field. With a strong foundation in programming and problem-solving skills, I am eager to apply my knowledge and contribute to innovative projects in a dynamic work environment.

Please contact me if you have any questions about the project or me.

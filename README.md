# Edge SQA Assignment - Parabank User Registration and Login Test

This project contains an automated test for the Parabank website that handles user registration and login functionality using CSV data and generates an HTML report using ExtentReports.

## Prerequisites

- JetBrains Rider IDE
- .NET SDK (Core or .NET 6+ recommended)
- Google Chrome installed
- ChromeDriver compatible with your Chrome version
- Windows OS (for hardcoded file paths like `D:\...`)

## Setup Instructions

1. **Create a New Console Project in Rider**

   - Launch JetBrains Rider
   - Select "New Solution"
   - Choose **Console Application (.NET Core)**
   - Name your project and click **Create**

2. **Install Required NuGet Packages**

   - Open the `NuGet` tab or press `Ctrl+Alt+N`
   - Search and install the following packages:
     - `Selenium.WebDriver`
     - `Selenium.WebDriver.ChromeDriver`
     - `ExtentReports` (from `AventStack`)
     - `CsvHelper`

3. **Add the Code**

   - Replace the contents of `Program.cs` with the provided test code
   - Ensure your `UserData` class is also in the same file or namespace

4. **CSV File Setup**

   - Create a folder `D:\sqa\` on your computer
   - Save your user data CSV file as `User_Registration_Data.csv` in that folder
   - Example headers:
     ```
     First Name,Last Name,Address,City,State,Zip Code,Phone Number,SSN,Username,Password,Confirm Password
     ```

5. **Update File Paths**

   In `Program.cs`:
   - Update the report file path:
     ```csharp
     string reportFilePath = $@"D:\sqa\{dateTime}.html";
     ```
   - Update the CSV file path:
     ```csharp
     using (var reader = new StreamReader(@"D:\sqa\User_Registration_Data.csv"))
     ```

   ✅ Make sure the folder `D:\sqa\` exists and contains your `.csv` file.

## Running the Tests

1. Press **Ctrl+Shift+F10** or click the **Run** icon next to `Main` to execute the test
2. The test will:
   - Open Chrome browser
   - Navigate to [Parabank](https://parabank.parasoft.com/parabank/index.htm)
   - Register users using data from the CSV file
   - Log out and then log in using registered credentials
   - Validate successful login
   - Log each step and result in an HTML report

## Test Report

After running the test, an HTML report will be generated at the location specified in reportFilePath. This report contains detailed information about each test step and its outcome.

## Troubleshooting

- ❗ Ensure ChromeDriver version matches your installed Chrome browser
- ❗ File paths must exist — create folders manually if needed
- ❗ CSV must have correct headers and formatting
- ✔️ Try running as Administrator if file access is restricted

## Notes

This project was developed by **Tahmidul Islam**  
Department of CSE, Jagannath University  
Automated with Selenium, CsvHelper, and ExtentReports using C# in Rider IDE.

# Edge SQA Assignment - Parabank User Registration and Login Test

This project contains an automated test for the Parabank website that handles user registration and login functionality using CSV data. The test logs each step and result into an HTML report using ExtentReports.

## Prerequisites

- JetBrains Rider IDE (or any C# IDE)
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
   - Ensure the `UserData` class is included in the same file or project

4. **CSV File Setup**

   - Save your user data CSV file as `User_Data.csv` directly under `D:\`
   - Example CSV headers (must match exactly):
     ```
     First Name,Last Name,Address,City,State,Zip Code,Phone Number,SSN,Username,Password,Confirm Password
     ```

5. **Verify Hardcoded Paths**

   - In `Program.cs`, the following paths are hardcoded:
     - Report:
       ```csharp
       string reportFilePath = $@"D:\{dateTime}.html";
       ```
     - CSV File:
       ```csharp
       using (var reader = new StreamReader(@"D:\User_Data.csv"))
       ```
   ✅ Make sure the file `D:\User_Data.csv` exists and is formatted properly.

## Running the Tests

1. Press **Ctrl+Shift+F10** or click the **Run** icon next to `Main` in Rider
2. The test will:
   - Launch Chrome browser
   - Navigate to [Parabank](https://parabank.parasoft.com/parabank/index.htm)
   - Register users from the CSV data
   - Log out and log in using registered credentials
   - Validate login success
   - Write a detailed report to an HTML file in `D:\`

## Test Report

After execution, an HTML report will be generated under `D:\` with the timestamp in its filename (e.g., `D:\20250509_153000.html`).  
It includes detailed steps, actions, and pass/fail status using ExtentReports.

## Troubleshooting

- ❗ Ensure your ChromeDriver version matches your installed Chrome browser
- ❗ CSV file path and headers must match exactly
- ❗ Folder `D:\` must exist; create it manually if needed
- ❗ Run the IDE as Administrator if encountering access issues

## Notes

This project was developed by **Tahmidul Islam**  
Department of CSE, Jagannath University  
Automated using **Selenium**, **CsvHelper**, and **ExtentReports** in C# with Rider IDE.

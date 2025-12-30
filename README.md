## Formula Validator

Bracket-based formula validator to verify whether formulas are well-formed.

---

## Problem Description

A formula composed of the characters ()[]{} is considered well-formed if:
   - Every opening bracket has a corresponding closing bracket of the same type
   - Brackets are closed in the correct order

---

## Solution Approach - Algorithm: Stack

The solution uses a Stack data structure to validate formulas:

   - Iterate through each character of the formula
   - If it is an opening bracket (([{)), push it onto the stack
   - If it is a closing bracket ()]}):
      ° Verify that the stack is not empty
      ° Pop the last opening bracket
      ° Verify that it matches the closing bracket type
   - At the end, the stack must be empty
   
   # Complexity:
   - Time: O(n), where n is the length of the formula
   - Space: O(n) in the worst case (all opening brackets)

---

## Applied SOLID Principles

Single Responsibility Principle (SRP)
   FormulaValidator: Responsible only for validating formulas
   FileFormulaReader: Responsible only for reading files

Open/Closed Principle (OCP)
   The system is extensible through interfaces
   New validators can be added without modifying existing code

Liskov Substitution Principle (LSP)
   Implementations can be substituted by their interfaces

Interface Segregation Principle (ISP)
   Small and specific interfaces (IDelimiterValidator, IFormulaReader)

Dependency Inversion Principle (DIP)
   High-level modules depend on abstractions, not concrete implementations


---


## Design Patterns

- Strategy Pattern: Validation behavior can be changed by implementing DelimiterValidator
- Dependency Injection: Dependencies are injected via constructors

- Project Structure

validatedelimiters/
├── validatedelimiters.Core/
│ ├── DelimiterValidator.cs
│ ├── FileFormulaReader.cs
│ ├── IDelimiterValidator.cs
│ └── IFormulaReader.cs
├── validatedelimiters.Tests/
│ ├── DelimiterValidatorTests.cs
│ └── TestData/
│ 
└── README.txt

---

## How to Run

- Requirements
   .NET 6.0 or higher

- Build
   bash - "dotnet build"

- Run Tests
   dotnet test

- Run Tests with Detailed Output
   dotnet test --logger "console;verbosity=detailed"

---

## Design Decisions

   - Use of Dictionary for mapping: Enables O(1) lookup for bracket pairs
   - Ignoring non-relevant characters: Formulas may contain other characters
   - Empty formula is valid: Design decision based on mathematical logic
   - Separation of responsibilities: Validation and file reading are separated

---

## Test Cases

- Valid

   (), [], {}
   ()[]{} – multiple pairs
   ([]) – correctly nested
   {[()]} – multiple nesting levels

- Invalid

   (] – mismatched types
   ([)] – incorrect order
   ((()) – missing closing bracket
   ()) – closing without opening



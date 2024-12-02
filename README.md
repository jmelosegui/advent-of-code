# 🎄 Advent of Code 🎅

This repository contains solutions for the [Advent of Code](https://adventofcode.com/) challenges implemented in C#.
This is where I solve daily programming puzzles during the holiday season. Each day brings a new challenge to enhance problem-solving and coding skills.

## 🧑‍💻 Code Challenges
Each challenge is solved using CSharp. The code for each day can be found in the `Challenges` directory:

```bash
AdventOfCode
├── Challenges
│   ├── Challenge.cs
│   ├── Day01
│   │   ├── Day01.cs
│   │   ├── Day01.input
│   │   └── Day01.test
├── Extensions
│   ├── ServiceCollectionExtensions.cs
├── Program.cs
├── AdventOfCode.csproj
```
- `AdventOfCode/Program.cs`: The main entry point of the application.
- `Challenges/`: Directory containing the challenge input and test files.
- `AdventOfCode2024/`: Directory containing the challenge implementations and extensions.

## Getting Started 🕯️

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### Running the Solutions

1. Clone the repository:
    ```sh
    git clone https://github.com/jmelosegui/AdventOfCode.git
    cd AdventOfCode
    ```

2. Build the project:
    ```sh
    dotnet build
    ```

3. Run the solutions:
    ```sh
    dotnet run --project AdventOfCode -- <challenge-day> 
    ```

## Adding New Challenges 🎁

1. Create a new directory for the day under `Challenges/` (e.g., `Challenges/Day04/`).
2. Add the input and test files for the challenge in the new directory.
3. Implement the challenge in a new class under `<YEAR>/AdventOfCode/Challenges/`.

## Example

Here is an example of how to run the solution for Day 04:

```sh
dotnet run --project AdventOfCode -- Day04
```
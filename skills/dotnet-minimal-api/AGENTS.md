# Agent Instructions

## Purpose

If something in the project surprises you during development, alert the developer and suggest adding
a gotcha here to prevent future agents from hitting the same issue.

## Markdown

Run `npx prettier --write <file>` on any Markdown file you create or edit before committing.

## Gotchas

- Parallel `dotnet test` runs can contend on shared build outputs under `template/artifacts`. Prefer
  sequential test execution when verifying changes unless you isolate the output paths first.

# Script to extract Git changes and prepare them for AI commit message generation

# Define the output file path
$outputFile = "gitChanges.txt"

# Define the prompt text to prepend to the file
$promptText = @"
Generate a commit message. Ensure that it includes a precise and informative subject line that succinctly summarizes the crux of the changes.
The commit message should follow the conventional commit format: <type>[scope]: <description>.
Ignore all white space changes.

Examples:
fix(authentication): add 403 forbidden error cases
feat(ai): generate commit messages from changes

Use bullet points to list changes verbose. Focus on the features, visual elements, types of changes, and interactions with data and UI. Cite relevant sources if available. Do not use any introductory or closing phrases, just a pure description.

There should be a TL;DR line provides a concise summary of the overall purpose of the changes as the first in the list of bullet points
"@

# Check if we're in a Git repository
if (-not (Test-Path -Path ".git" -PathType Container)) {
    Write-Error "Not in a Git repository. Please run this script from a Git repository root."
    exit 1
}

# First, check if there are any staged changes
$stagedChanges = git diff --cached --name-status

# If there are staged changes, get the detailed diff of staged changes
if ($stagedChanges) {
    Write-Host "Found staged changes. Getting detailed diff..."
    $gitDiff = git diff --cached
}
# Otherwise, get all pending changes
else {
    Write-Host "No staged changes found. Getting all pending changes..."
    $gitDiff = git diff
}

# If there are no changes at all, inform the user
if (-not $gitDiff) {
    Write-Host "No changes detected in the repository."
    exit 0
}

# Combine the prompt and the git diff
$fileContent = $promptText + "`n`n" + $gitDiff

# Write the content to the output file
Set-Content -Path $outputFile -Value $fileContent

Write-Host "Git changes have been written to $outputFile with the AI prompt."
read-host "Press Enter to exit"

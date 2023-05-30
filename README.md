# Anti-Corruption Layer

This project is an API working as an anti-corruption layer in order to intermediate 2 or more systems communicating with each other.

#	Setup
It's necessary to set some values in appsettings' files, as much in Api project as in Test project. 
These keys are:
GitHubBearer: token access generated in GitHub settings. It's possible to generate in GitHub Panel > Settings > Developer settings > Personal Access Tokens.
GitHubOwner: owner of the GitHub account. Usually is the username.

In test class you can change the repository's name in order to test on your GitHub account the way you want.
# Lock Note

## 🚀 Up and running

### Locally

Run the cosmos emulator in docker by running the script `cosmosdblocalsetupScript.sh`

```bash
./scripts/cosmosdblocalsetupScript.sh
```

Then just run the project and everything should work.

### In the Cloud ☁️

Use azure cli to create service principal:

```bash
az ad sp create-for-rbac --name "github-actions-bicep-deploy" --role contributor \
  --scopes /subscriptions/b5e84508-5c27-44d5-bcbe-25762d6f6de9 --sdk-auth
```

#### Create secrets

this will output a JSON object this entire object should be put in to the secret `AZURE_CREDENTIALS`




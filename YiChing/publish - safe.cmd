rem See g:\My Drive\Sys\Backup\AndroidKeystore\
dotnet restore
dotnet publish -f net8.0-android34.0 -c Release -p:AndroidKeyStore=true -p:AndroidSigningKeyStore=yiching.keystore -p:AndroidSigningKeyAlias=yiching -p:AndroidSigningKeyPass={password} -p:AndroidSigningStorePass={password}
pause
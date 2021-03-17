timestamps {
    node {
        def suffix = "tsu-${BUILD_NUMBER}"
        stage('Clone repository') {
            checkout scm
        }
        docker.image('mcr.microsoft.com/dotnet/core/sdk:3.1').inside() {
            stage('Restore Dependencies') {
                sh 'dotnet restore'
            }
            stage('Build') {
                sh "dotnet build Projects/Dotmim.Sync.Core/Dotmim.Sync.Core.csproj -c Release -f netstandard2.0 -o ${WORKSPACE}/${BUILD_NUMBER}/Dotmim.Sync.Core --version-suffix ${suffix}"
                sh "dotnet build Projects/Dotmim.Sync.SqlServer/Dotmim.Sync.SqlServer.csproj -c Release -f netstandard2.0 -o ${WORKSPACE}/${BUILD_NUMBER}/Dotmim.Sync.SqlServer --version-suffix ${suffix}"
                sh "dotnet build Projects/Dotmim.Sync.SqlServer.ChangeTracking/Dotmim.Sync.SqlServer.ChangeTracking.csproj -c Release -f netstandard2.0 -o ${WORKSPACE}/${BUILD_NUMBER}/Dotmim.Sync.SqlServer.ChangeTracking --version-suffix ${suffix}"
                sh "dotnet build Projects/Dotmim.Sync.Sqlite/Dotmim.Sync.Sqlite.csproj -c Release -f netstandard2.0 -o ${WORKSPACE}/${BUILD_NUMBER}/Dotmim.Sync.Sqlite --version-suffix ${suffix}"
                sh "dotnet build Projects/Dotmim.Sync.Web.Client/Dotmim.Sync.Web.Client.csproj -c Release -f netstandard2.0 -o ${WORKSPACE}/${BUILD_NUMBER}/Dotmim.Sync.Web.Client --version-suffix ${suffix}"
                sh "dotnet build Projects/Dotmim.Sync.Web.Server/Dotmim.Sync.Web.Server.csproj -c Release -f netstandard2.0 -o ${WORKSPACE}/${BUILD_NUMBER}/Dotmim.Sync.Web.Server --version-suffix ${suffix}"
            }
            stage('Push to Azure') {
                pushPackage("${WORKSPACE}/${BUILD_NUMBER}", "Dotmim.Sync.Core")
                pushPackage("${WORKSPACE}/${BUILD_NUMBER}", "Dotmim.Sync.SqlServer")
                pushPackage("${WORKSPACE}/${BUILD_NUMBER}", "Dotmim.Sync.SqlServer.ChangeTracking")
                pushPackage("${WORKSPACE}/${BUILD_NUMBER}", "Dotmim.Sync.Sqlite")
                pushPackage("${WORKSPACE}/${BUILD_NUMBER}", "Dotmim.Sync.Web.Client")
                pushPackage("${WORKSPACE}/${BUILD_NUMBER}", "Dotmim.Sync.Web.Server")
            }
        }
    }
}

def String pushPackage(String path, String packageName) {
    def file = getPackageFile(path, packageName)
    sh "dotnet nuget push ${file} -s https://pkgs.dev.azure.com/ffm-vic-apps/_packaging/nuget-feed/nuget/v3/index.json -k junk-key"
}

def String getPackageFile(String path, String packageName) {
    def file = sh returnStdout: true, script: "find ${path}/${packageName} -name ${packageName}*.nupkg -not -name *.symbols.nupkg -printf '%p'"
    return file
}

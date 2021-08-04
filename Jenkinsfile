pipeline
{
    agent any
    triggers {
        githubPush()
    }
    
    stages {
        stage('Restore packages'){
            steps{
                sh 'dotnet restore RedisSampleAPI.sln' 
             }
        }
        stage('Clean'){
            steps{
                sh 'dotnet clean RedisSampleAPI.sln --configuration Release'
             }
        }
        stage('Build'){
            steps{
                sh 'dotnet build RedisSampleAPI.sln --configuration Release --no-restore'
             }
        }
        stage('Publish'){
            steps{
                sh 'dotnet publish RedisSampleAPI/RedisSampleAPI.csproj --configuration Release --no-restore'
             }
        }
    }
}

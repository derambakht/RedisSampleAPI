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
     post {  
         always {  
             echo 'This will always run'  
         }  
         success {  
             echo 'This will run only if successful'  
         }  
         failure {  
             mail bcc: '', body: "<b>Example</b><br>Project: ${env.JOB_NAME} <br>Build Number: ${env.BUILD_NUMBER} <br> URL de build: ${env.BUILD_URL}", cc: '', charset: 'UTF-8', from: '', mimeType: 'text/html', replyTo: '', subject: "ERROR CI: Project name -> ${env.JOB_NAME}", to: "foo@foomail.com";  
         }  
         unstable {  
             echo 'This will run only if the run was marked as unstable'  
         }  
         changed {  
             echo 'This will run only if the state of the Pipeline has changed'  
             echo 'For example, if the Pipeline was previously failing but is now successful'  
         }  
     }  
}

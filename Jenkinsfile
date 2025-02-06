pipeline {
    agent any

    environment {
        // Define variables de entorno, por ejemplo, la ruta donde publicar la API o la carpeta destino.
        PUBLICATION_DIR = '/var/www/timelyIO'
    }

    stages {
        stage('Clonar Repositorio') {
            steps {
                // Se usa la credencial definida anteriormente.
                git branch: 'main', credentialsId: 'github-credentials', url: 'https://github.com/aletsrangeI/TimelyIO.API'
            }
        }
        stage('Restaurar Dependencias') {
            steps {
                dir('TimelyIO.API') {
                    sh 'dotnet restore'
                }
            }
        }
        stage('Compilar') {
            steps {
                dir('TimelyIO.API') {
                    sh 'dotnet build --configuration Release'
                }
            }
        }
        stage('Ejecutar Pruebas') {
            steps {
                // Si tienes proyectos de pruebas, ejecútalos.
                dir('TimelyIO.API') {
                    sh 'dotnet test'
                }
            }
        }
        stage('Publicar') {
            steps {
                dir('TimelyIO.API') {
                    // Publica la aplicación para producción.
                    sh 'dotnet publish --configuration Release --output publish'
                }
            }
        }
        stage('Desplegar') {
            steps {
                // Ejemplo: copiar los archivos publicados a un directorio y reiniciar el servicio.
                sh """
                  sudo cp -r $(pwd)/TimelyIO.API/publish/* ${PUBLICATION_DIR}/
                  sudo systemctl restart miapi.service
                """
            }
        }
    }
    post {
        success {
            echo 'El despliegue fue exitoso.'
        }
        failure {
            echo 'Ocurrió un error en el proceso de CI/CD.'
        }
    }
}

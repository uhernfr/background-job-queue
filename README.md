# background-job-queue
A project to create an Excel file in the background and use SignalR to download it when it's done.

The idea is to request a report (an Excel file in this example), queue it in a task queue, and receive the response at a different endpoint using SignalR in the frontend application.

## projects ##
background-job-queue - backend service (http://localhost:5054/swagger/index.html)
background-job-queue-web - frontend  (https://localhost:7115)

## endpoints ##
http://localhost:5054/generate - will make the request, return a job id and generate an excel file.
http://localhost:5054/wait-for-file/{jobId} - send jobId and receive file to download

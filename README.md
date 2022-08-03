Team :

Patient and PatientHistory - Akshaya Dupati

FAQ and Medicine - Santhini

Departments and Doctor - Germanjeet

Appointments - Elliot-Brown Ford


To view the patient list : 

1. https://localhost:44300/api/PatientData/ListPatients
2. curl -k https://localhost:44300/api/PatientData/ListPatients


----------------------------------------------------

To find the patient : 

1. https://localhost:44300/api/PatientData/FindPatient

----------------------------------------------------

To delete the patient : 

1. curl -k -d "" https://localhost:44300/api/PatientData/DeletePatient/1

----------------------------------------------------

To add the patient : 

1. curl -k -d @patient.json -H "Content-type:application/json" https://localhost:44300/api/PatientData/AddPatient

----------------------------------------------------

To update the patient : ( In progress )

1. curl -k -d @patient.json -H "Content-type:application/json" https://localhost:44300/api/PatientData/UpdatePatient/4

----------------------------------------------------

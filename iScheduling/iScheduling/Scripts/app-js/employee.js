$(document).ready(() => {
    getEmployeeInfo = (empId) => {
        var url = `http://localhost:52901/Employee/Edit?employeeId=${empId}`;

        $.get(url, (data) => {
            $('#edit-employee-modal').html(data);

            $('#edit-employee').modal('show');
        });
    }

    deleteEmployee = (empId) => {
        var url = `http://localhost:52901/Employee/_Delete?employeeId=${empId}`;

        $.get(url, (data) => {
            $('#delete-employee-modal').html(data);

            $('#delete-employee').modal('show');
        });
    }
});


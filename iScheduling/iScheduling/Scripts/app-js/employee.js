$(document).ready(() => {
    getEmployeeInfo = (empId) => {
        var url = `/Employee/Edit?employeeId=${empId}`;

        $.get(url, (data) => {
            $('#edit-employee-modal').html(data);

            $('#edit-employee').modal('show');
        });
    }

    deleteEmployee = (empId) => {
        var url = `/Employee/_Delete?employeeId=${empId}`;

        $.get(url, (data) => {
            $('#delete-employee-modal').html(data);

            $('#delete-employee').modal('show');
        });
    }
});


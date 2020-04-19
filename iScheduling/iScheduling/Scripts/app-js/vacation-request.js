$(document).ready(() => {

    viewVacationRequestToApprove = (requestId) => { 
        var url = `/VacationRequest/Approve?requestId=${requestId}`;

        $.get(url, (data) => {
            $('#approve-vacation-request-modal').html(data);

            $('#approve-vacation-request').modal('show');
        });
    }

    viewVacationRequestToReject = (requestId) => {
        var url = `/VacationRequest/Reject?requestId=${requestId}`;

        $.get(url, (data) => {
            $('#reject-vacation-request-modal').html(data);

            $('#reject-vacation-request').modal('show');
        });
    }

    createVacationRequest = () => {
        var url = `/VacationRequest/Create`;

        $.get(url, (data) => {
            $('#create-vacation-request-modal').html(data);

            $('#create-vacation-request').modal('show');

            var today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

            $('#vacationStartDatePicker').datepicker({
                startDate: today,
                autoclose: true,
                format: "MM-dd-yyyy"
            }).datepicker("setDate", today);

            $('#vacationEndDatePicker').datepicker({
                startDate: today,
                autoclose: true,
                format: "MM-dd-yyyy"
            }).datepicker("setDate", today);
        });
    }

    createRequest = () => {
        var startDate = $('#vacationStartDatePicker').val();
        var endDate = $('#vacationEndDatePicker').val();

        var url = `/VacationRequest/Create`;
        var data = {
            StartDate: startDate,
            EndDate: endDate
        };

        $.post(url, data, (res) => {
            if (!res.IsSuccess)
                alert(res.Message);

            // reload list requests page
            $('#create-vacation-request').modal('hide');

            window.location.reload();
        });
    }

    approve = (requestId) => {
        var comment = $('#txtApproveVacationComment').val();
        
        var url = `/VacationRequest/Approve`;
        var data = {
            RequestId: requestId,
            Comment: comment
        };

        $.post(url, data, (res) => {
            if (!res.IsSuccess)
                alert(res.Message);

            // reload list requests page
            $('#approve-vacation-request').modal('hide');

            window.location.reload();
        });
    }

    reject = (requestId) => {
        var comment = $('#txtRejectVacationComment').val();

        var url = `/VacationRequest/Reject`;
        var data = {
            RequestId: requestId,
            Comment: comment
        };

        $.post(url, data, (res) => {
            if (!res.IsSuccess)
                alert(res.Message);

            // reload list requests page
            $('#reject-vacation-request').modal('hide');

            window.location.reload();
        });
    }
});
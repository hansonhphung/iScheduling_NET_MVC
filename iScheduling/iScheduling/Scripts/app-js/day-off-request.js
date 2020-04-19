$(document).ready(() => {

    viewDORDetailToApprove = (requestId) => { 
        var url = `/DayOffRequest/Approve?requestId=${requestId}`;

        $.get(url, (data) => {
            $('#approve-dor-modal').html(data);

            $('#approve-day-off-request').modal('show');
        });
    }

    viewDORDetailToReject = (requestId) => {
        var url = `/DayOffRequest/Reject?requestId=${requestId}`;

        $.get(url, (data) => {
            $('#reject-dor-modal').html(data);

            $('#reject-day-off-request').modal('show');
        });
    }

    requestDayOff = (shiftId) => {
        var url = `/DayOffRequest/Create?shiftId=${shiftId}`;

        $.get(url, (data) => {
            $('#request-day-off-modal').html(data);

            $('#create-day-off-request').modal('show');
        });
    }

    createRequest = () => {
        var shiftId = $('#shiftId').val();

        var reason = $('#txtReason').val();

        var url = `/DayOffRequest/Create`;
        var data = {
            ShiftId: shiftId,
            Reason: reason
        };

        $.post(url, data, (res) => {
            if (!res.IsSuccess)
                alert(res.Message);

            // reload list requests page
            var selectedEndDate = $('#shiftEndDatePicker').val();
            $('#shiftEndDatePicker').datepicker("setDate", selectedEndDate);

            $('#create-day-off-request').modal('hide');
        });
    }

    approve = (requestId) => {
        var comment = $('#txtApproveDORComment').val();
        var shiftId = $('#shiftId').val();

        var url = `/DayOffRequest/Approve`;
        var data = {
            RequestId: requestId,
            ShiftId: shiftId,
            Comment: comment
        };

        $.post(url, data, (res) => {
            if (!res.IsSuccess)
                alert(res.Message);

            // reload list requests page
            $('#approve-day-off-request').modal('hide');

            window.location.reload();
        });
    }

    reject = (requestId) => {
        var comment = $('#txtRejectDORComment').val();
        var shiftId = $('#shiftId').val();

        var url = `/DayOffRequest/Reject`;
        var data = {
            RequestId: requestId,
            ShiftId: shiftId,
            Comment: comment
        };

        $.post(url, data, (res) => {
            if (!res.IsSuccess)
                alert(res.Message);

            // reload list requests page
            $('#reject-day-off-request').modal('hide');

            window.location.reload();
        });
    }
});
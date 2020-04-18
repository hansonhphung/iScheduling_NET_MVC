$(document).ready(() => {

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
        alert('Request approved');
    }

    reject = (requestId) => {
        alert('Request rejected');
    }
});
$(document).ready(() => {

    $('#start-time-picker .cell').on('click', function () {
        $('#start-time-picker .cell').removeClass('select');
        $(this).addClass('select');

        $('#start-time-picker').removeClass('show-time-picker');
        $('#start-time-picker').addClass('hide-time-picker');


        var selectedTime = $(this).text();

        $('#start-time').val(selectedTime);
    });

    $('#end-time-picker .cell').on('click', function () {
        $('#end-time-picker .cell').removeClass('select');
        $(this).addClass('select');

        $('#end-time-picker').removeClass('show-time-picker');
        $('#end-time-picker').addClass('hide-time-picker');


        var selectedTime = $(this).text();

        $('#end-time').val(selectedTime);
    });

    chooseStartTime = () => {
        $('#start-time-picker').removeClass('hide-time-picker');
        $('#start-time-picker').addClass('show-time-picker');
    }

    chooseEndTime = () => {
        $('#end-time-picker').removeClass('hide-time-picker');
        $('#end-time-picker').addClass('show-time-picker');
    }

    deleteShift = (shiftId) => {
        var url = `/Shift/Delete?shiftId=${shiftId}`;

        $.get(url, (deleteRes) => {
            var lstShiftURL = `/Shift/List`;

            var shiftDate = $('#date-of-shift').val();

            var data = { date: shiftDate };

            $.post(lstShiftURL, data, (lstShiftRes) => {
                $('#list-shifts').html(lstShiftRes);

                $('#add-edit-shift').modal('hide');

                alert(deleteRes.Message);
            });
        });
    }
});
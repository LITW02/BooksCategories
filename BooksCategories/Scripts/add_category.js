$(function () {
    var categories = $("#categories").val().split(',');
    $("#catInput").keyup(function () {
        var value = $(this).val();
        var match = categories.filter(function (cat) {
            return cat === value;
        });
        $("#submitButton").prop('disabled', match.length);
    });
});
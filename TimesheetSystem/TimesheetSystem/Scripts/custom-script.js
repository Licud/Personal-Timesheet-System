
$(function () {

    $(".change").click(function () {
        var type = $(this).attr("data-type");
        var path = "";

        if (type == 1)
        {
            path = "/Tasks/UpdateStatus"
        } else
        {
            path = "/Projects/UpdateStatus"
        }

        // Get the id from the link
        var recordToChange = $(this).attr("data-id");
        
        if (recordToChange != '') {
            // Perform the ajax post
            
            $.post(path, { "id" : recordToChange },
                function (data) {
                    // Successful requests get here, update elements
                    $('#status-' + data.identifier).text(data.newStatus);           
             });
        }
    });
});
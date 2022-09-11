const DeleteToDo = (event,element) => {
    event.preventDefault();
    var id = $(element).closest('tr').data('id');
    console.log(id);
    //$($('#myModal').find('form')).find('input[type="hidden"]').val(id);
    $('#myModal').find('form').attr('action', '/Home/Delete/' + id);
    $('#myModal').modal('show');
}


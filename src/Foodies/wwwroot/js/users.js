var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#dt-users').DataTable({
        "ajax": {
            "url": "/api/user",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "fullname", "width": "25%" },
            { "data": "email", "width": "25%" },
            { "data": "phonenumber", "width": "15%" },
            {
                "data": { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var currentDate = new Date().getTime();
                    var lockoutEnd = new Date(data.lockoutEnd).getTime();
                    if (lockoutEnd > currentDate) {
                        return ` <div class="text-center">
                                    <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick='LockUnlockUser('${data.id}')'>
                                        <i class="fas fa-lock-open"></i> Unlock
                                    </a>
                                </div>`;
                    }
                    else {
                        return ` <div class="text-center">
                                    <a class="btn btn-success text-white" style="cursor:pointer; width:100px;" onclick='LockUnlockUser('${data.id}')'>
                                        <i class="fas fa-lock"></i> Lock
                                    </a>
                                </div>`;
                    }
                }, "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}

function LockUnlockUser(url) {
    swal({
        title: "Are you sure you want to lock this User?",
        text: "Are you sure you want to lock this User!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'POST',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
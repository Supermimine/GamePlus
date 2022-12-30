function ValidateInput(id, icon, title, text) {
    if (document.getElementById(id).value == "" || document.getElementById(id).value == null) {
        Swal.fire({
            icon: icon,
            title: title,
            text: text,
        });
        return false;
    }
    return true;
}

function ComfirmeDelete(url) {
    Swal.fire({
        title: 'Êtes-vous sûr',
        text: 'Vous ne pourrez pas revenir en arrière !',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: getComputedStyle(document.documentElement).getPropertyValue('--important'),
        cancelButtonColor: getComputedStyle(document.documentElement).getPropertyValue('--important'),
        confirmButtonText: 'Confirmer!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        toastr.success("Succès");
                        window.location.reload();
                    }
                    else {
                        toastr.error("Erreur");
                        window.location.reload();
                    }
                }
            })
        }

    });
}
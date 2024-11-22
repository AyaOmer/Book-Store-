document.addEventListener('DOMContentLoaded', function () {
    const createButton = document.querySelector('.btn-dark');
    createButton.addEventListener('mouseover', function () {
        createButton.style.backgroundColor = '#555';
    });
    createButton.addEventListener('mouseout', function () {
        createButton.style.backgroundColor = '#343a40';
    });

    const editButtons = document.querySelectorAll('.btn-primary');
    editButtons.forEach(button => {
        button.addEventListener('mouseover', function () {
            button.style.backgroundColor = '#0056b3';
        });
        button.addEventListener('mouseout', function () {
            button.style.backgroundColor = '#007bff';
        });
    });

    const deleteButtons = document.querySelectorAll('.btn-danger');
    deleteButtons.forEach(button => {
        button.addEventListener('mouseover', function () {
            button.style.backgroundColor = '#c82333';
        });
        button.addEventListener('mouseout', function () {
            button.style.backgroundColor = '#dc3545';
        });
    });
});

var button = document.getElementById('edit-button');
var time = 300;
var headers = [...document.getElementsByClassName('header')];
var icons = [...document.getElementsByClassName('icon')];


function tableClick() {
    if (button.classList.contains('fa-table'))
    toTable();
    else
        toEdit();
}

function toEdit() {
    table.style.left = '-150%';
    document.getElementById('add-books').style.left = '50%';
    button.classList.remove('fa-pen');
    button.classList.add('fa-table');
}

function toTable() {
    document.getElementById('add-books').style.left = '150%';
    table.style.left = '50%';
    button.classList.add('fa-pen');
    button.classList.remove('fa-table');
}
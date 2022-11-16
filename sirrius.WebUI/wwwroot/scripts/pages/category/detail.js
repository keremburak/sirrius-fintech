$(document).ready(function () {
    $(".crm-shortcut-btn").attr('href', '/category/index');
    $(".crm-shortcut-btn").attr('data-original-title', 'Geri Dön').tooltip('update');
    $(".crm-shortcut-btn").find('i').removeClass('fa-plus').addClass('fa-arrow-left');
});
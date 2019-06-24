$(function () {
  $('[data-toggle="tooltip"]').tooltip();

  $(".navbar-toggler.rounded-circle").click(function () {
    $(this).toggleClass('rotate-arrow');
  });
  if (document.location.href.endsWith('/Admin/') ||
    document.location.href.endsWith('/Admin') ||
    document.location.href.includes('/Log')) {
    $('body > .container').removeClass('container').addClass('container-fluid');
  }
});
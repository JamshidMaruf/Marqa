// Sidebar Toggle
$(document).ready(function () {
  $('#sidebarCollapse').on('click', function () {
    $('#sidebar').toggleClass('active');
    $('#content').toggleClass('active');
  });

  // Auto-hide alerts after 5 seconds
  setTimeout(function () {
    $('.alert').fadeOut('slow');
  }, 5000);

  // Confirm delete actions
  $('.btn-delete').on('click', function (e) {
    if (!confirm('Are you sure you want to delete this item?')) {
      e.preventDefault();
    }
  });

  // DataTables initialization (if you add DataTables later)
  if ($.fn.DataTable) {
    $('.data-table').DataTable({
      responsive: true,
      pageLength: 10,
      language: {
        search: "Search:",
        lengthMenu: "Show _MENU_ entries",
        info: "Showing _START_ to _END_ of _TOTAL_ entries",
        paginate: {
          first: "First",
          last: "Last",
          next: "Next",
          previous: "Previous"
        }
      }
    });
  }

  // Form validation
  $('form').on('submit', function (e) {
    var isValid = true;
    $(this).find('[required]').each(function () {
      if ($(this).val() === '') {
        isValid = false;
        $(this).addClass('is-invalid');
      } else {
        $(this).removeClass('is-invalid');
      }
    });

    if (!isValid) {
      e.preventDefault();
      alert('Please fill in all required fields.');
    }
  });

  // Remove validation error on input
  $('[required]').on('input', function () {
    if ($(this).val() !== '') {
      $(this).removeClass('is-invalid');
    }
  });
});


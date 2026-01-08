let deleteId = null;
let deleteUrl = null;

function openDeleteModal(element) {
  deleteId = element.getAttribute('data-id');
  deleteUrl = element.getAttribute('data-url');

  const name = element.getAttribute('data-name');
  document.getElementById('deleteItemName').innerText = name;

  const modal = new bootstrap.Modal(document.getElementById('deleteModal'));
  modal.show();
}

// Make sure this runs after DOM is loaded
document.addEventListener('DOMContentLoaded', function () {
  const confirmDeleteBtn = document.getElementById('confirmDeleteBtn');

  if (confirmDeleteBtn) {
    confirmDeleteBtn.addEventListener('click', async () => {
      try {
        // Get anti-forgery token
        const token = document.querySelector('input[name="__RequestVerificationToken"]');

        // Create form data
        const formData = new FormData();
        formData.append('id', deleteId);
        if (token) {
          formData.append('__RequestVerificationToken', token.value);
        }

        // Send delete request
        const response = await fetch(deleteUrl, {
          method: 'POST',
          body: formData,
          headers: {
            'RequestVerificationToken': token ? token.value : ''
          }
        });

        if (response.redirected) {
          // If redirected (which happens on success), reload the page
          window.location.href = response.url;
        } else if (response.ok) {
          // Close modal
          const modalEl = document.getElementById('deleteModal');
          const modalInstance = bootstrap.Modal.getInstance(modalEl);
          if (modalInstance) {
            modalInstance.hide();
          }

          // Reload page
          location.reload();
        } else {
          throw new Error('Delete failed with status: ' + response.status);
        }

      } catch (error) {
        console.error('Delete error:', error);
        alert('Error deleting company: ' + error.message);
      }
    });
  }
});

function openDetailsModal(button) {
  const id = button.dataset.id;
  const baseUrl = button.dataset.url;

  fetch(`${baseUrl}?id=${id}`)
    .then(res => res.text())
    .then(html => {
      document.getElementById("detailsModalContent").innerHTML = html;

      new bootstrap.Modal(
        document.getElementById("detailsModal")
      ).show();
    });
}

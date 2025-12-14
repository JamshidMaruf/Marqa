async function deleteCompany(companyId) {
  try {
    // Get CSRF token
    const token = document.querySelector('input[name="__RequestVerificationToken"]');

    // Create form data
    const formData = new URLSearchParams();
    formData.append('id', companyId);
    if (token) {
      formData.append('__RequestVerificationToken', token.value);
    }

    const response = await fetch('@Url.Action("Delete")', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded'
      },
      body: formData
    });

    if (response.ok) {
      // Close all modals
      const modals = document.querySelectorAll('.modal.show');
      modals.forEach(modal => {
        const bsModal = bootstrap.Modal.getInstance(modal);
        if (bsModal) bsModal.hide();
      });

      // Reload page
      location.reload();
    } else {
      const text = await response.text();
      console.error('Error response:', text);
      alert('Error deleting company: ' + response.status);
    }
  } catch (error) {
    console.error('Error:', error);
    alert('Error deleting company: ' + error.message);
  }
}
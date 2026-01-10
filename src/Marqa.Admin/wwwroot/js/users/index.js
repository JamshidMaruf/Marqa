let selectedUserId = null;
let blockStatus = null;

function openBlockModal(element) {
    selectedUserId = element.getAttribute('data-id');
    blockStatus = element.getAttribute('data-status') === 'true';
    
    const message = blockStatus
        ? "Are you sure you want to block this user?"
        : "Are you sure you want to unblock this user?";
    
    document.getElementById("blockMessage").innerText = message;
    
    const modal = new bootstrap.Modal(document.getElementById("blockModal"));
    modal.show();
}

document.getElementById("confirmBlockBtn").addEventListener("click", function () {
  fetch('/Users/ChangeBlockStatus', {
    method: 'PATCH',
    headers: {
      'Content-Type': 'application/x-www-form-urlencoded'
    },
    body: `id=${selectedUserId}`
  })
    .then(() => location.reload());
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

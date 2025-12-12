// Auto-hide alerts after 5 seconds
setTimeout(function () {
  const alerts = document.querySelectorAll('.alert');
  alerts.forEach(alert => {
    alert.style.transition = 'opacity 0.5s';
    alert.style.opacity = '0';
    setTimeout(() => alert.remove(), 500);
  });
}, 5000);

// Phone input formatting
const phoneInput = document.getElementById('phone');

phoneInput.addEventListener('input', function (e) {
  let value = e.target.value;

  // Remove all non-numeric characters except +
  value = value.replace(/[^0-9+]/g, '');

  // If value doesn't start with +, add it
  if (value && !value.startsWith('+')) {
    value = '+' + value;
  }

  // Remove multiple + signs
  if (value.startsWith('++')) {
    value = '+' + value.replace(/\+/g, '');
  }

  e.target.value = value;
});

// Form submission - remove + before sending to server
document.querySelector('form').addEventListener('submit', function (e) {
  let phoneValue = phoneInput.value;
  // Remove + before sending to server
  phoneValue = phoneValue.replace(/\+/g, '');
  phoneInput.value = phoneValue;
});

console.log("AUTH.JS LOADED ✔");

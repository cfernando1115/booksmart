const $roleInput = $('.role-name');
$roleInput.change(() => {
    const $role = $roleInput.val();
    $role === "Member"
        ? $('.membership-type').removeAttr('hidden')
        : $('.membership-type').attr('hidden', 'hidden');
})
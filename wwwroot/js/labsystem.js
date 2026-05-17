function filterChems(val) {
    const rows = document.querySelectorAll('#chem-table tr[data-state]');

    rows.forEach(r => {
        r.style.display = r.textContent.toLowerCase().includes(val.toLowerCase())
            ? ''
            : 'none';
    });
}

function filterChemState(state, el) {
    document.querySelectorAll('.pill').forEach(p => p.classList.remove('active'));

    el.classList.add('active');

    const rows = document.querySelectorAll('#chem-table tr[data-state]');

    rows.forEach(r => {
        r.style.display = (state === 'all' || r.dataset.state === state)
            ? ''
            : 'none';
    });
}
window.renderLineChart = (canvasId, labels, data, label) => {
    const ctx = document.getElementById(canvasId);
    if (!ctx) return;

    if (ctx.chart) {
        ctx.chart.destroy();
    }

    ctx.chart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: label,
                data: data,
                borderColor: '#667eea',
                backgroundColor: 'rgba(102, 126, 234, 0.2)',
                tension: 0.3,
                fill: true
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    });
};

window.renderBarChart = (canvasId, labels, data, label) => {
    const ctx = document.getElementById(canvasId);
    if (!ctx) return;

    if (ctx.chart) {
        ctx.chart.destroy();
    }

    ctx.chart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: label,
                data: data,
                backgroundColor: '#764ba2'
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    });
};

window.renderPieChart = (canvasId, labels, data, label) => {
    const ctx = document.getElementById(canvasId);
    if (!ctx) return;

    if (ctx.chart) {
        ctx.chart.destroy();
    }

    ctx.chart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                label: label,
                data: data,
                backgroundColor: [
                    '#667eea',
                    '#f5576c',
                    '#43e97b',
                    '#fa709a',
                    '#feca57',
                    '#54a0ff'
                ]
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'bottom'
                }
            }
        }
    });
};
function createOrderItemCard(name, unit, quantity, index) {
    return `
                <div class="mt-2">
                    <p><b>Наименование</b>: ${name}</p>
                    <p><b>Unit</b>: ${unit}</p>
                    <p><b>Количество</b>: ${quantity}</p>
                    <button class="btn-sm btn-danger" onclick="deleteOrderItem(${index})">Удалить</button>
                </div>
            `
}
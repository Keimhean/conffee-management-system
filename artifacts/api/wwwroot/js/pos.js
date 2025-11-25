const API_URL = '/api';
let products = [];
let cart = [];
let selectedCategory = 'All';
let searchQuery = '';
let showReportsFlag = false;

// Initialize
document.addEventListener('DOMContentLoaded', () => {
    loadProducts();
    loadTransactions();
    loadStats();
    setupEventListeners();
});

function setupEventListeners() {
    document.getElementById('search').addEventListener('input', (e) => {
        searchQuery = e.target.value;
        filterProducts();
    });
    
    document.getElementById('cash-amount').addEventListener('input', updateChange);
}

async function loadProducts() {
    try {
        const response = await fetch(`${API_URL}/products`);
        products = await response.json();
        renderCategories();
        filterProducts();
    } catch (error) {
        console.error('Error loading products:', error);
    }
}

function renderCategories() {
    const categories = [
        { name: 'All', icon: '/icons/cart.svg' },
        { name: 'Coffee', icon: '/icons/coffee-icon.svg' },
        { name: 'Tea', icon: '/icons/tea.svg' },
        { name: 'Pastry', icon: '/icons/Pastry.svg' },
        { name: 'Snack', icon: '/icons/Snack.svg' }
    ];
    const container = document.getElementById('categories');
    container.innerHTML = categories.map(cat => 
        `<button class="category-btn ${cat.name === selectedCategory ? 'active' : ''}" 
                 onclick="selectCategory('${cat.name}')">
            <img src="${cat.icon}" alt="" class="category-icon">
            ${cat.name}
        </button>`
    ).join('');
}

function selectCategory(category) {
    selectedCategory = category;
    renderCategories();
    filterProducts();
}

function filterProducts() {
    let filtered = products;
    
    if (selectedCategory !== 'All') {
        filtered = filtered.filter(p => p.category === selectedCategory);
    }
    
    if (searchQuery) {
        filtered = filtered.filter(p => 
            p.name.toLowerCase().includes(searchQuery.toLowerCase())
        );
    }
    
    renderProducts(filtered);
}

function renderProducts(productList) {
    const container = document.getElementById('products-grid');
    container.innerHTML = productList.map(product => `
        <div class="product-card" onclick="addToCart(${product.id})">
            <div style="position: relative;">
                <img src="${product.imageUrl}" alt="${product.name}" class="product-image">
                <span class="product-category">${product.category}</span>
                <button class="product-delete" onclick="deleteProduct(event, ${product.id})" title="Delete product" aria-label="Delete product">
                    <img src="/icons/icon-delete.svg" class="delete-icon" alt="Delete">
                </button>
            </div>
            <div class="product-info">
                <div class="product-name">${product.name}</div>
                <div class="product-price">$${product.price.toFixed(2)}</div>
            </div>
        </div>
    `).join('');
}

async function deleteProduct(e, productId) {
    e.stopPropagation();
    if (!confirm('Delete this product? This will hide it from the menu.')) return;

    try {
        const res = await fetch(`${API_URL}/products/${productId}`, { method: 'DELETE' });
        if (res.ok) {
            // remove locally and re-render
            products = products.filter(p => p.id !== productId);
            filterProducts();
            alert('Product deleted (soft-delete)');
        } else if (res.status === 404) {
            alert('Product not found');
        } else {
            const txt = await res.text();
            throw new Error(txt || 'Delete failed');
        }
    } catch (err) {
        console.error('Error deleting product:', err);
        alert('Failed to delete product');
    }
}

function addToCart(productId) {
    const product = products.find(p => p.id === productId);
    if (!product) return;
    
    const existingItem = cart.find(item => item.product.id === productId);
    if (existingItem) {
        existingItem.quantity++;
    } else {
        cart.push({ product, quantity: 1 });
    }
    
    renderCart();
    updateTotals();
}

function renderCart() {
    const container = document.getElementById('cart-items');
    document.getElementById('cart-count').textContent = `${cart.length} items`;
    
    if (cart.length === 0) {
        container.innerHTML = '<div style="text-align: center; color: #9CA3AF; padding: 40px;">No items in cart</div>';
        return;
    }
    
    container.innerHTML = cart.map((item, index) => `
        <div class="cart-item">
            <div class="cart-item-info">
                <div class="cart-item-name">${item.product.name}</div>
                <div class="cart-item-price">$${item.product.price.toFixed(2)} × ${item.quantity}</div>
            </div>
            <div class="cart-item-controls">
                <button class="qty-btn" onclick="decreaseQuantity(${index})">−</button>
                <span style="min-width: 28px; text-align: center; font-weight: bold;">${item.quantity}</span>
                <button class="qty-btn" onclick="increaseQuantity(${index})">+</button>
                <button class="qty-btn" onclick="removeFromCart(${index})" style="color: #EF4444;">🗑️</button>
            </div>
        </div>
    `).join('');
}

function increaseQuantity(index) {
    cart[index].quantity++;
    renderCart();
    updateTotals();
}

function decreaseQuantity(index) {
    if (cart[index].quantity > 1) {
        cart[index].quantity--;
    } else {
        cart.splice(index, 1);
    }
    renderCart();
    updateTotals();
}

function removeFromCart(index) {
    cart.splice(index, 1);
    renderCart();
    updateTotals();
}

function updateTotals() {
    const subtotal = cart.reduce((sum, item) => sum + (item.product.price * item.quantity), 0);
    const tax = subtotal * 0.10;
    const total = subtotal + tax;
    
    document.getElementById('subtotal').textContent = `$${subtotal.toFixed(2)}`;
    document.getElementById('tax').textContent = `$${tax.toFixed(2)}`;
    document.getElementById('total').textContent = `$${total.toFixed(2)}`;
    
    document.getElementById('checkout-btn').disabled = cart.length === 0;
}

function showPayment() {
    if (cart.length === 0) return;
    document.getElementById('payment-section').style.display = 'block';
    document.getElementById('checkout-btn').style.display = 'none';
}

function cancelPayment() {
    document.getElementById('payment-section').style.display = 'none';
    document.getElementById('checkout-btn').style.display = 'block';
    document.getElementById('cash-amount').value = '';
    document.getElementById('change-display').style.display = 'none';
}

function updateChange() {
    const cashAmount = parseFloat(document.getElementById('cash-amount').value) || 0;
    const total = cart.reduce((sum, item) => sum + (item.product.price * item.quantity), 0) * 1.10;
    const change = Math.max(0, cashAmount - total);
    
    if (cashAmount >= total && cashAmount > 0) {
        document.getElementById('change-display').style.display = 'block';
        document.getElementById('change').textContent = change.toFixed(2);
        document.getElementById('complete-btn').disabled = false;
    } else {
        document.getElementById('change-display').style.display = 'none';
        document.getElementById('complete-btn').disabled = true;
    }
}

async function completePayment() {
    const cashierName = document.getElementById('cashier-name').value;
    if (!cashierName) {
        alert('Please enter cashier name');
        return;
    }
    
    const cashAmount = parseFloat(document.getElementById('cash-amount').value);
    const transaction = {
        cashierName,
        cashReceived: cashAmount,
        items: cart.map(item => ({
            id: 0,
            productId: item.product.id,
            productName: item.product.name,
            price: item.product.price,
            quantity: item.quantity
        }))
    };
    
    try {
        const response = await fetch(`${API_URL}/transactions`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(transaction)
        });
        
        if (response.ok) {
            document.getElementById('success-message').style.display = 'block';
            document.getElementById('payment-section').style.display = 'none';
            
            setTimeout(() => {
                cart = [];
                renderCart();
                updateTotals();
                document.getElementById('success-message').style.display = 'none';
                document.getElementById('checkout-btn').style.display = 'block';
                document.getElementById('cashier-name').value = '';
                document.getElementById('cash-amount').value = '';
                loadTransactions();
                loadStats();
            }, 2000);
        }
    } catch (error) {
        console.error('Error completing transaction:', error);
        alert('Failed to complete transaction');
    }
}

function toggleReports() {
    showReportsFlag = !showReportsFlag;
    document.getElementById('reports-section').style.display = showReportsFlag ? 'block' : 'none';
    if (showReportsFlag) {
        loadTransactions();
        loadStats();
    }
}

async function loadStats() {
    try {
        const response = await fetch(`${API_URL}/transactions/stats`);
        const stats = await response.json();
        
        document.getElementById('stats-grid').innerHTML = `
            <div class="stat-card">
                <div class="stat-label">Total Transactions</div>
                <div class="stat-value">${stats.totalTransactions}</div>
            </div>
            <div class="stat-card">
                <div class="stat-label">Total Revenue</div>
                <div class="stat-value" style="color: #F59E0B;">$${stats.totalRevenue.toFixed(2)}</div>
            </div>
            <div class="stat-card">
                <div class="stat-label">Average Order</div>
                <div class="stat-value" style="color: #8B5CF6;">$${stats.averageOrder.toFixed(2)}</div>
            </div>
            <div class="stat-card">
                <div class="stat-label">Items Sold</div>
                <div class="stat-value" style="color: #10B981;">${stats.totalItems}</div>
            </div>
        `;
    } catch (error) {
        console.error('Error loading stats:', error);
    }
}

async function loadTransactions() {
    try {
        const response = await fetch(`${API_URL}/transactions`);
        const transactions = await response.json();
        
        document.getElementById('transactions-list').innerHTML = `
            <h3 style="margin: 20px 0 12px; font-size: 20px;">Recent Transactions</h3>
            ${transactions.slice(0, 10).map(t => `
                <div class="transaction-card">
                    <div>
                        <div style="font-weight: 600; margin-bottom: 6px;">Transaction #${t.id}</div>
                        <div style="color: #6B7280; font-size: 13px;">
                            👤 ${t.cashierName} • 🕒 ${new Date(t.transactionDate).toLocaleString()}
                        </div>
                    </div>
                    <div style="display:flex; gap:8px; align-items:center;">
                        <div style="background: linear-gradient(135deg, #FEF3C7, #FDE68A); padding: 8px 16px; border-radius: 8px; font-weight: bold; font-size: 20px; color: #D97706;">
                            $${t.total.toFixed(2)}
                        </div>
                        <button class="transaction-delete" onclick="deleteTransaction(${t.id})" title="Delete transaction" aria-label="Delete transaction">
                            <img src="/icons/icon-delete.svg" class="delete-icon" alt="Delete">
                        </button>
                    </div>
                </div>
            `).join('')}
        `;
    } catch (error) {
        console.error('Error loading transactions:', error);
    }
}

async function deleteTransaction(transactionId) {
    if (!confirm('Delete this transaction? This will remove it permanently.')) return;

    try {
        const res = await fetch(`${API_URL}/transactions/${transactionId}`, { method: 'DELETE' });
        if (res.ok) {
            alert('Transaction deleted');
            loadTransactions();
            loadStats();
        } else if (res.status === 404) {
            alert('Transaction not found');
        } else {
            const txt = await res.text();
            throw new Error(txt || 'Delete failed');
        }
    } catch (err) {
        console.error('Error deleting transaction:', err);
        alert('Failed to delete transaction');
    }
}

function refreshData() {
    loadProducts();
    loadTransactions();
    loadStats();
}

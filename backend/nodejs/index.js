const express = require('express');
const cors = require('cors');
const app = express();
const port = 5000;

// Dummy customer data with different scenarios
const customerData = {
  Customer1: {
    DiscountVersion: 'V1',
    ShippingVersion: 'V1',
    TaxVersion: 'V2',
  },
  Customer2: {
    DiscountVersion: 'V2',
    ShippingVersion: 'V1',
    TaxVersion: 'V1',
  },
  Customer3: {
    DiscountVersion: 'V1',
    ShippingVersion: 'V2',
    TaxVersion: 'V2',
  },
  Customer4: {
    // Customer4 does not have specific preferences and will use the default versions
  },
};

// Business rule logic
class DiscountV1Strategy {
  calculateDiscount(totalPrice) {
    return totalPrice * 0.1; // 10% discount
  }
}

class DiscountV2Strategy {
  calculateDiscount(totalPrice) {
    return totalPrice * 0.15; // 15% discount
  }
}

class ShippingV1Strategy {
  calculateShippingCost(totalWeight) {
    return totalWeight * 5; // $5 per kg shipping cost
  }
}

class ShippingV2Strategy {
  calculateShippingCost(totalWeight) {
    return totalWeight * 7; // $7 per kg shipping cost
  }
}

class TaxV1Strategy {
  calculateTax(totalPrice) {
    return totalPrice * 0.2; // 20% tax
  }
}

class TaxV2Strategy {
  calculateTax(totalPrice) {
    return totalPrice * 0.15; // 15% tax
  }
}

// Strategy Factory
class StrategyFactory {
  getDiscountStrategy(version) {
    if (version === 'V1') {
      return new DiscountV1Strategy();
    } else if (version === 'V2') {
      return new DiscountV2Strategy();
    }
    // Add other versions here if needed
    return new DiscountV1Strategy(); // Default strategy
  }

  getShippingStrategy(version) {
    if (version === 'V1') {
      return new ShippingV1Strategy();
    } else if (version === 'V2') {
      return new ShippingV2Strategy();
    }
    // Add other versions here if needed
    return new ShippingV1Strategy(); // Default strategy
  }

  getTaxStrategy(version) {
    if (version === 'V1') {
      return new TaxV1Strategy();
    } else if (version === 'V2') {
      return new TaxV2Strategy();
    }
    // Add other versions here if needed
    return new TaxV1Strategy(); // Default strategy
  }
}

app.use(express.json());
app.use(cors());

app.get('/api/calculate-order/:customerId/:totalPrice/:totalWeight', (req, res) => {
  const { customerId, totalPrice, totalWeight } = req.params;
  const customerPreferences = customerData[customerId] || {};

  const strategyFactory = new StrategyFactory();
  const discountStrategy = strategyFactory.getDiscountStrategy(customerPreferences.DiscountVersion);
  const shippingStrategy = strategyFactory.getShippingStrategy(customerPreferences.ShippingVersion);
  const taxStrategy = strategyFactory.getTaxStrategy(customerPreferences.TaxVersion);

  const discountAmount = discountStrategy.calculateDiscount(parseFloat(totalPrice));
  const shippingCost = shippingStrategy.calculateShippingCost(parseFloat(totalWeight));
  const taxAmount = taxStrategy.calculateTax(parseFloat(totalPrice));

  const orderTotal = parseFloat(totalPrice) - discountAmount + shippingCost + taxAmount;

  res.json({ discountAmount, shippingCost, taxAmount, orderTotal });
});

app.listen(port, () => {
  console.log(`Backend server running on http://localhost:${port}`);
});

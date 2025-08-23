ALTER TABLE products
    ALTER COLUMN created_at TYPE TIMESTAMPTZ USING created_at::timestamptz,
    ALTER COLUMN created_at SET DEFAULT NOW(); 

ALTER TABLE carts 
    ALTER COLUMN created_at TYPE TIMESTAMPTZ USING created_at::timestamptz,
    ALTER COLUMN created_at SET DEFAULT NOW();

ALTER TABLE orders 
    ALTER COLUMN created_at TYPE TIMESTAMPTZ USING created_at::timestamptz,
    ALTER COLUMN created_at SET DEFAULT NOW();
namespace UnitTestForStructApp
{
    public struct ProductStruct
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public int Amount { get; set; }

        public ProductStruct(int id,string name,decimal cost, int amount) {
            this.Id = id;
            this.Name = name;
            this.Cost = cost;
            this.Amount = amount;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var tmp = obj as ProductStruct?;
            if (!tmp.HasValue)
                return false;
            ProductStruct p = tmp.Value;
            return p.Id == this.Id
                && p.Name == this.Name 
                && p.Cost == this.Cost 
                && p.Amount == this.Amount;
        }

        public override int GetHashCode()
        {
            // recomended on SO
            int hash = 23, coPrime = 37;
            hash = hash * coPrime + this.Id.GetHashCode();
            hash = hash * coPrime + this.Cost.GetHashCode();
            hash = hash * coPrime + this.Name.GetHashCode();
            hash = hash * coPrime + this.Amount.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return "ProductName: " + Name + ", ProductCost:" + Cost + ", Amount:" + Amount;
        }
    }
}

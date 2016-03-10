﻿using System;

public class Fuel
{
    double gallonsPurchased,
           costOfTank,
           milesDriven;

    // Default constructor -- Sets properties to zero
	public Fuel()
	{
        gallonsPurchased = costOfTank = milesDriven = 0;
	}

    // Mutator methods
    public void setGallonsPurchased(double gallons)
    {
        gallonsPurchased = gallons;
    }
    public void setCostOfTank(double cost)
    {
        costOfTank = cost;
    }
    public void setMilesDriven(double miles)
    {
        milesDriven = miles;
    }

    // Accessor Methods
    public double getGallonsPurchased()
    {
        return gallonsPurchased;
    }
    public double getCostOfTank()
    {
        return costOfTank;
    }
    public double getMilesDriven()
    {
        return milesDriven;
    }

    // Calculate Miles Per Gallon
    public double milesPerGallon()
    {
        return (milesDriven / gallonsPurchased);
    }

    //Calculate Cost Per Gallon
    public double costPerGallon()
    {
        return (costOfTank / milesDriven);
    }
}

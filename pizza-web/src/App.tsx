import React, { useState } from "react";
import { PizzaItem } from "./PizzaItem";
import { Pizza } from "./types/Pizza";
  
let pizzas: Pizza[] =[{
  id: 1, name: 'Cheese pizza', description: 'very cheesy'
},
{
  id: 2, name: 'Al Tono pizza', description: 'lots of tuna'
}];

const App = () => {
  const data = pizzas.map((pizza, index) => <PizzaItem key={index} pizza={pizza} />)

  return (
    <React.Fragment>
      {data}
    </React.Fragment>
  )
}

export default App;
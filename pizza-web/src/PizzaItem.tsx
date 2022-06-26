import React, { useState } from "react";
import { Pizza } from "./types/Pizza";

type Props = {
  pizza: Pizza
};
  
export const PizzaItem = ({ pizza }: Props) => {
  const [data, setData] = useState(pizza);
  const [dirty, setDirty] = useState(false);

  function update(value: string, fieldName: string, obj: Pizza) {
    setData({ ...obj, [fieldName] : value});
    setDirty(true);
  }

  function onSave() {
    setDirty(false);
  }

  return (
    <React.Fragment>
      <div>
        <h3>
          <input onChange={(e) => update(e.target.value, 'name', data)} value={data.name} />
        </h3>
        <div>  
          <input onChange={(e) => update(e.target.value, 'description', data)} value={data.description} />
        </div>
        {dirty ?
          <div><button onClick={onSave}>Save</button></div> : null
        }
      </div>
    </React.Fragment>
  )
}
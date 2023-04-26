import axios from "axios";
import React, { useEffect, useState, useRef } from "react";

function App() {
  const [state, setState] = useState({
    Prompt: "",
    response: "",
  });
  // const [response, setResponse] = useState("");
  const dataFetched = useRef(false);

  const getPrompt = async (query) => {
    try {
      const response = await axios.get(
        `https://localhost:7179/api/OpenApi/UseChatGPT?query=${query}`
      );
      console.log(response.data);
      setState({
        ...state,
        Prompt: "",
        response: response.data,
      });
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    if (dataFetched.current) return;
    dataFetched.current = true;
    console.log("useEffect called");
  }, []);

  const handleButtonClick = () => {
    getPrompt(state.Prompt);
  };

  const handleInputChange = (event) => {
    setState({
      ...state,
      Prompt: event.target.value,
      response: state.response,
    });
  };

  return (
    <>
      <label htmlFor="exampleInputEmail1">Enter Your Prompt</label>
      <input
        type="text"
        value={state.Prompt}
        onChange={handleInputChange}
        required
      />
      <button onClick={handleButtonClick} className="btn btn-primary">
        Get Result
      </button>
      <p>{state.response}</p>
    </>
  );
}

export default App;

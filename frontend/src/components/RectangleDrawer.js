import React, { useEffect, useState } from "react";
import axios from "axios";
import PerimeterDisplay from "./PerimeterDisplay";

const RectangleDrawer = () => {
  const [rectangle, setRectangle] = useState({ x: 20, y: 20, width: 100, height: 100 });
  const [perimeter, setPerimeter] = useState(0);
  const [resizeDirection, setResizeDirection] = useState(null);
  const [mouseStart, setMouseStart] = useState({ x: 0, y: 0 });
  const [saveFeedback, setSaveFeedback] = useState("");

    useEffect(() => {
        axios.get("http://localhost:5141/rectangle")
        .then(response => {
            setRectangle({ x: 20, y: 20, width: response.data.width, height: response.data.height })
            calculatePerimeter(response.data);
        })
        .catch(error => console.error("Error fetching rectangle:", error));
    }, []);

    const handleMouseMove = (e) => {
        if (!resizeDirection) return;

        const deltaX = e.clientX - mouseStart.x;
        const deltaY = e.clientY - mouseStart.y;

        if (resizeDirection === "right") {
        setRectangle((prev) => ({
            ...prev,
            width: prev.width + deltaX > 0 ? prev.width + deltaX : prev.width,
        }));
        } else if (resizeDirection === "left") {
        setRectangle((prev) => ({
            ...prev,
            width: prev.width - deltaX > 0 ? prev.width - deltaX : prev.width,
            x: prev.x + deltaX < prev.x + prev.width ? prev.x + deltaX : prev.x,
        }));
        } else if (resizeDirection === "bottom") {
        setRectangle((prev) => ({
            ...prev,
            height: prev.height + deltaY > 0 ? prev.height + deltaY : prev.height,
        }));
        } else if (resizeDirection === "top") {
        setRectangle((prev) => ({
            ...prev,
            height: prev.height - deltaY > 0 ? prev.height - deltaY : prev.height,
            y: prev.y + deltaY < prev.y + prev.height ? prev.y + deltaY : prev.y,
        }));
    }

    calculatePerimeter({ width: rectangle.width, height: rectangle.height })
    setMouseStart({ x: e.clientX, y: e.clientY });
  };

  const handleMouseDown = (e, direction) => {
    if (e.button !== 0) {
        return;
    }

    setResizeDirection(direction);
    setMouseStart({ x: e.clientX, y: e.clientY });
  };

  const handleMouseUp = () => {
    setResizeDirection(null);
  };

  const saveRectangle = async () => {
    try {
      setSaveFeedback("Saving...");
      const response = await axios.put("http://localhost:5141/rectangle", {
        width: rectangle.width,
         height: rectangle.height
        });

      if (response.status === 200) {
        setSaveFeedback("Rectangle saved successfully");
      } else if(response.status === 400) {
        setSaveFeedback(`Failed to save rectangle: ${response.data.message}`);
      }
    } catch (error) {
      const message = `Failed to save rectangle. ${error.response.data.message}`
      console.error(message);
      setSaveFeedback(message);
    }
  };

  const calculatePerimeter = ({ width, height }) => {
    setPerimeter(2 * (width + height));
  };

  return (
    <div>
      <svg
        width="100%"
        height={window.innerHeight * 0.8}
        style={{ border: "1px solid black", position: "relative" }}
        onMouseMove={resizeDirection ? handleMouseMove : null}
        onMouseUp={handleMouseUp}
      >
        <rect
          x={rectangle.x}
          y={rectangle.y}
          width={rectangle.width}
          height={rectangle.height}
          fill="lightblue"
        />

        {/* Top */}
        <rect
          x={rectangle.x + 5}
          y={rectangle.y}
          width={rectangle.width - 10}
          height={5}
          fill="black"
          cursor="ns-resize"
          onMouseDown={(e) => handleMouseDown(e, "top")}
        />
        
        {/* Bottom */}
        <rect
          x={rectangle.x + 5}
          y={rectangle.y + rectangle.height - 5}
          width={rectangle.width - 10}
          height={5}
          fill="black"
          cursor="ns-resize"
          onMouseDown={(e) => handleMouseDown(e, "bottom")}
        />
        
        {/* Left */}
        <rect
          x={rectangle.x}
          y={rectangle.y + 5}
          width={5}
          height={rectangle.height - 10}
          fill="black"
          cursor="ew-resize"
          onMouseDown={(e) => handleMouseDown(e, "left")}
        />
        
        {/* Right */}
        <rect
          x={rectangle.x + rectangle.width - 5}
          y={rectangle.y + 5}
          width={5}
          height={rectangle.height - 10}
          fill="black"
          cursor="ew-resize"
          onMouseDown={(e) => handleMouseDown(e, "right")}
        />
      </svg>
      <PerimeterDisplay perimeter={perimeter} />

      <div>
        {/* Save button */}
        <button onClick={saveRectangle} style={{ position: "flex"}}>
            Save Rectangle
        </button>

        {/* Feedback */}
        <div style={{ position: "flex"}}>
            <span>{saveFeedback}</span>
        </div>
      </div>
    </div>
  );
};

export default RectangleDrawer;
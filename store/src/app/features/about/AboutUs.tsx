import { Alert, AlertTitle, Button, ButtonGroup, List, ListItem, ListItemText, Typography } from "@mui/material";
import { Container } from "@mui/system";
import { useState } from "react";
import agent from "../../api/agent";

export default function AboutUs() {
    const [validationErrors, setValidationErrors] = useState<string[]>([]);

    function getValidationError() {
        agent.TestErrors.getValidationError()
            .then(() => console.log('should not see this'))
            .catch(error => setValidationErrors(error))
    }

    return (
<div>
  <meta charSet="utf-8" />
  <meta httpEquiv="X-UA-Compatible" content="IE=edge" />
  <title>About Us card || Learning robo</title>
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <link rel="stylesheet" href="style.css" />
  <style dangerouslySetInnerHTML={{__html: "\n@import url('https://fonts.googleapis.com/css2?family=Playfair+Display:wght@400;500;600;700&display=swap');\n\n*{\n    box-sizing: border-box;\n    padding: 0;\n    margin: 0;\n}\nbody{\n    font-family: 'Playfair Display', serif;\n    background-color: #F0F0F0;\n\n\n    align-content: center;\n    min-height: 100vh;\n}\nsection{\n    display: grid;\n    grid-template-columns: 1fr 1fr;\n    min-height: 70vh;\n    width: 50vw;\n    margin: 0 auto;\n    box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);\n    border-radius: 12px\n}\n.image{\n    background-color: #FFF;\n    display: flex;\n    border-radius: 12px 0 0 12px;\n}\n.image img{\nheight:50vh;\nmargin:50px auto\n}\n.content{\n    background-color: #000;\n    display: flex;\n    justify-content: center;\n    flex-direction: column;\n    align-items: center;\n    border-radius: 0  12px 12px 0;\n    color: #fff;\n}\n.content h2{\n    text-transform: uppercase;\n    font-size: 36px;\n    letter-spacing: 8px;\n    opacity: 0.9;\n}\n.content span{\n    height: 0.5px;\n    width: 80px;\n    background: #777;\n    margin: 30px 0;\n}\n.content p{\n    padding-bottom: 15px;\n    font-weight: 300;\n    opacity: 0.9;\n    width: 60%;\n    text-align: center;\n    margin: 0 auto;\n    line-height: 1.7;\n    color:#ffffff\n}\n.links{\n    margin: 15px 0;\n}\n.links li{\n    border: 2px solid #4158D0;\n    list-style: none;\n    border-radius: 5px;\n    padding: 10px 15px;\n    width: 160px;\n    text-align: center;\n}\n.links li a{\n    text-transform: uppercase;\n    color: #fff;\n    text-decoration: none;\n}\n.links li:hover{\n    border-color: #FFF300;\n}\n\n.vertical-line{\n    height: 30px;\n    width: 3px;\n    background: #C850C0;\n    margin: 0 auto;\n}\n.icons{\n    display: flex;\n    padding: 15px 0;\n}\n.icons li{\n    display: block;\n    padding: 5px;\n    margin: 5px;\n}\n.icons li i{\n    font-size: 26px;\n    opacity: 0.8;\n}\n.icons li i:hover{\n    color: #FF0000;\n    cursor: pointer;\n}\n\n\n/*****************/\n\n@media(max-width: 900px){\n    section{\n        grid-template-columns: 1fr;\n        width: 100%;\n        border-radius: none;\n    }\n    .image{\n        height: 100vh;\n        border-radius: none;\n    }\n    .content{\n        height: 100vh;\n        border-radius: none;\n    }\n    .content h2{\n        font-size: 20px;\n        margin-top: 50px;\n    }\n    .content span{\n        margin: 20px 0;\n    }\n    .content p{\n        font-size: 14px;\n    }\n    .links li a{\n        font-size: 14px;\n    }\n    .links{\n        margin: 5px 0;\n    }\n    .links li{\n        padding: 6px 10px;\n    }\n    .icons li i{\n        font-size: 15px;\n    }\n}\n.credit{\n    text-align: center;\n    color: #000;\n    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;\n  }\n  \n  .credit a{\n    text-decoration: none;\n    color:#000;\n    font-weight: bold;\n  } \n\n\n    " }} />
  <section>
    <div className="image">
      <img src="https://i.postimg.cc/gjJB0L38/unnamed-1-1.png" />
    </div>
    <div className="content">
      <h2>About Us</h2>
      <span>{/* line here */}</span>
      <p>We ensure customer satisfaction by adding value and honouring commitments at all times. We are committed to building shareholder value and maintaining high standards of corporate governance. We strive to be an eco-friendly organisation, inculcating good corporate citizenship.</p>
      <ul className="links">
        <li><a href="https://www.hexaware.com/about-us">Read More</a></li>
      </ul>
      
    </div>
  </section><br /><br />
</div>
    )
}
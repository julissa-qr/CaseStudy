
import { useAppDispatch, useAppSelector } from "../../store/configureStore";

export default function ContactUs() {
    const dispatch = useAppDispatch();
    const { data, title } = useAppSelector(state => state.counter);

    return (
<div>
  <meta charSet="UTF-8" />
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css" />
  <meta name="viewport"  />
  <style dangerouslySetInnerHTML={{__html: "\n       /* Google Font CDN Link */\n@import url('https://fonts.googleapis.com/css2?family=Poppins:wght@200;300;400;500;600;700&display=swap');\n*{\n  margin: 0;\n  padding: 0;\n  box-sizing: border-box;\n  font-family: \"Poppins\" , sans-serif;\n}\nbody{\n  min-height: 100vh;\n  width: 100%;\n  background: #F0F0F0	;\n  align-items: center;\n  justify-content: center;\n}\n.container{\n  width: 85%;\n  background: #fff;\n  border-radius: 6px;\n  padding: 20px 60px 30px 40px;\n  box-shadow: 0 5px 10px rgba(0, 0, 0, 0.2);\n}\n.container .content{\n  display: flex;\n  align-items: center;\n  justify-content: space-between;\n}\n.container .content .left-side{\n  width: 25%;\n  height: 100%;\n  display: flex;\n  flex-direction: column;\n  align-items: center;\n  justify-content: center;\n  margin-top: 15px;\n  position: relative;\n}\n.content .left-side::before{\n  content: '';\n  position: absolute;\n  height: 70%;\n  width: 2px;\n  right: -15px;\n  top: 50%;\n  transform: translateY(-50%);\n  background: #afafb6;\n}\n.content .left-side .details{\n  margin: 14px;\n  text-align: center;\n}\n.content .left-side .details i{\n  font-size: 30px;\n  color: #006CFF	;\n  margin-bottom: 10px;\n}\n.content .left-side .details .topic{\n  font-size: 18px;\n  font-weight: 500;\n}\n.content .left-side .details .text-one,\n.content .left-side .details .text-two{\n  font-size: 14px;\n  color: #afafb6;\n}\n.container .content .right-side{\n  width: 75%;\n  margin-left: 75px;\n}\n.content .right-side .topic-text{\n  font-size: 23px;\n  font-weight: 600;\n  color: #006CFF;\n}\n.right-side .input-box{\n  height: 50px;\n  width: 100%;\n  margin: 12px 0;\n}\n.right-side .input-box input,\n.right-side .input-box textarea{\n  height: 100%;\n  width: 100%;\n  border: none;\n  outline: none;\n  font-size: 16px;\n  background: #F0F1F8;\n  border-radius: 6px;\n  padding: 0 15px;\n  resize: none;\n}\n.right-side .message-box{\n  min-height: 110px;\n}\n.right-side .input-box textarea{\n  padding-top: 6px;\n}\n.right-side .button{\n  display: inline-block;\n  margin-top: 12px;\n}\n.right-side .button input[type=\"button\"]{\n  color: #fff;\n  font-size: 18px;\n  outline: none;\n  border: none;\n  padding: 8px 16px;\n  border-radius: 6px;\n  background: #3e2093;\n  cursor: pointer;\n  transition: all 0.3s ease;\n}\n.button input[type=\"button\"]:hover{\n  background: #5029bc;\n}\n\n@media (max-width: 950px) {\n  .container{\n    width: 90%;\n    padding: 30px 40px 40px 35px ;\n  }\n  .container .content .right-side{\n   width: 75%;\n   margin-left: 55px;\n}\n}\n@media (max-width: 820px) {\n  .container{\n    margin: 40px 0;\n    height: 100%;\n  }\n  .container .content{\n    flex-direction: column-reverse;\n  }\n .container .content .left-side{\n   width: 100%;\n   flex-direction: row;\n   margin-top: 40px;\n   justify-content: center;\n   flex-wrap: wrap;\n }\n .container .content .left-side::before{\n   display: none;\n }\n .container .content .right-side{\n   width: 100%;\n   margin-left: 0;\n }\n}\n\n\n   " }} />
  <div className="container">
    <div className="content" >
      <div className="left-side">
        <div className="address details">
          <i className="fas fa-map-marker-alt" />
          <div className="topic">Address</div>
          <div className="text-one">San Angel 240, Valle San Agustín,</div>
          <div className="text-two"> 25215 Saltillo, Coah.</div>
        </div>
        <div className="phone details">
          <i className="fas fa-phone-alt" />
          <div className="topic">Phone</div>
          <div className="text-one">8441254698</div>
          <div className="text-two">8445614799</div>
        </div>
        <div className="email details">
          <i className="fas fa-envelope" />
          <div className="topic">Email</div>
          <div className="text-one">joseG1@hexaware.com</div>
          <div className="text-two">julissaQ@hexaware.com</div>
        </div>
      </div>
      <div className="right-side">
        <div className="topic-text">Send us a message</div>
        <p>Please feel free to contact usif there is any problem with your order or if you need something else.</p>
      </div>
    </div>
  </div>
</div>
      );
}
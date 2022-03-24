class Transition < ApplicationRecord
    belongs_to  :state_machine
    belongs_to  :prev_state, class_name: "State" 
    belongs_to  :next_state, class_name: "State" 
    has_one     :customer_action, dependent: :destroy
    has_one     :trigger, dependent: :destroy
    accepts_nested_attributes_for :customer_action, allow_destroy: true
    accepts_nested_attributes_for :trigger, allow_destroy: true
end

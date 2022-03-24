class ScenarioCategoriesController < ApplicationController
  before_action :set_scenario_category, only: [:show, :edit, :update, :destroy]

  # GET /scenario_categories
  # GET /scenario_categories.json
  def index
    @scenario_categories = ScenarioCategory.all
  end

  # GET /scenario_categories/1
  # GET /scenario_categories/1.json
  def show
    @state_machines = StateMachine.where(scenario_category_id: params[:id])
  end

  # GET /scenario_categories/new
  def new
    @scenario_category = ScenarioCategory.new
  end

  # GET /scenario_categories/1/edit
  def edit
  end

  # POST /scenario_categories
  # POST /scenario_categories.json
  def create
    @scenario_category = ScenarioCategory.new(scenario_category_params)

    respond_to do |format|
      if @scenario_category.save
        format.html { redirect_to @scenario_category, notice: 'Scenario category was successfully created.' }
        format.json { render :show, status: :created, location: @scenario_category }
      else
        format.html { render :new }
        format.json { render json: @scenario_category.errors, status: :unprocessable_entity }
      end
    end
  end

  # PATCH/PUT /scenario_categories/1
  # PATCH/PUT /scenario_categories/1.json
  def update
    respond_to do |format|
      if @scenario_category.update(scenario_category_params)
        format.html { redirect_to @scenario_category, notice: 'Scenario category was successfully updated.' }
        format.json { render :show, status: :ok, location: @scenario_category }
      else
        format.html { render :edit }
        format.json { render json: @scenario_category.errors, status: :unprocessable_entity }
      end
    end
  end

  # DELETE /scenario_categories/1
  # DELETE /scenario_categories/1.json
  def destroy
    @scenario_category.destroy
    respond_to do |format|
      format.html { redirect_to scenario_categories_url, notice: 'Scenario category was successfully destroyed.' }
      format.json { head :no_content }
    end
  end

  private
    # Use callbacks to share common setup or constraints between actions.
    def set_scenario_category
      @scenario_category = ScenarioCategory.find(params[:id])
    end

    # Never trust parameters from the scary internet, only allow the white list through.
    def scenario_category_params
      params.require(:scenario_category).permit(:info)
    end
end
